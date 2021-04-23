using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private TextAsset _TestLevelJson;
    [SerializeField] private Tile[] _TilePrefabs;
    
    private GameObject _levelGameObject;
    
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

    void Start()
    {
        GameData data = JsonUtility.FromJson<GameData>(_TestLevelJson.ToString());
        SpawnLevel(data.LevelData[0]);
        CreatePaths();
    }

    private void CreatePaths()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnLevel(LevelData levelData)
    {
        _levelGameObject = new GameObject("Level");
        _levelGameObject.transform.SetParent(transform);
        
        for (int i = 0; i < levelData.Tiles.Length; i++)
        {
            int tileTypeIndex = GetTileTypeIndex(levelData.Tiles[i]);

            Tile tile = Instantiate(_TilePrefabs[tileTypeIndex], _levelGameObject.transform);
            Vector3 tilePosition = new Vector3(i % levelData.Width, 0, i / levelData.Width);
            tile.name = levelData.Tiles[i];
            tile.transform.position = tilePosition;
        }
    }

    private int GetTileTypeIndex(string tileName)
    {
        int index = 0;
        
        switch (tileName)
        {
            case "Normal":
                index = 0;
                break;
            case "Waypoint":
                index = 1;
                break;
            case "Spawnpoint":
                index = 2;
                break;
        }

        return index;
    }
}
