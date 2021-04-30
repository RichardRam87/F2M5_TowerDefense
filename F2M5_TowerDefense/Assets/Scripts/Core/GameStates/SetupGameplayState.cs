using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameplayState : GameplayState
{
    [SerializeField] private TextAsset _TestLevelJson;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _levelToLoad = 0;
    
    private GameData _gameData;
    private LevelData _activeLevelData;
    
    public struct GameData
    {
        public LevelData[] LevelData;
    }
    [System.Serializable]
    public struct LevelData
    {
        public int Width;
        public int Height;
        public string[] Tiles;
    }
    
    //TODO: Implement CreatePaths Method
    public override void StateEnter()
    {
        base.StateEnter();

        _gameData = CreateGameDataFromTextAsset();
        SpawnLevel(_gameData.LevelData[_levelToLoad]);
        // CreatePaths();
        UpdateCameraPosition();
    }

    private void SpawnLevel(LevelData levelData)
    {
        if (_levelSpawner.SpawnLevel(levelData))
            _activeLevelData = levelData;
        else 
            Debug.Log("Could not spawn level");
    }
    
    private void UpdateCameraPosition()
    {
        Vector3 cameraPosition = new Vector3();
        cameraPosition.x = _activeLevelData.Width / 2f - 0.5f;
        cameraPosition.y = _camera.transform.position.y; //default height 10
        cameraPosition.z = -_activeLevelData.Height / 2f;

        _camera.transform.position = cameraPosition;
    }

    private GameData CreateGameDataFromTextAsset()
    {
        return JsonUtility.FromJson<GameData>(_TestLevelJson.ToString());
    }
}
