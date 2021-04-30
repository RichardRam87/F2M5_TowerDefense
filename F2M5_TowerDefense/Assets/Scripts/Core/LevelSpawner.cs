using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private Tile[] _TilePrefabs;

    private GameObject _levelGameObject;
    
    // todo: Method should return Tile[]
    public bool SpawnLevel(SetupGameplayState.LevelData levelData)
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
        return true;
    }

    private int GetTileTypeIndex(string tileName)
    {
        int index = 0;

        if (tileName.Equals("Normal"))
            index = 0;
        else if (tileName.Contains("Path"))
            index = 1;
        else if (tileName.Equals("Spawnpoint"))
            index = 2;

        return index;
    }
}
