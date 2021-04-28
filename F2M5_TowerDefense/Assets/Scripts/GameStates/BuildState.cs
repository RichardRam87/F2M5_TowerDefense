using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;

public class BuildState : GameplayState
{
    [SerializeField] private TowerSpawner _towerSpawner;
    [SerializeField] private ResourceHandler _resourceHandler;

    private Tower _towerToBuild;
    private void Awake()
    {
        OnStateEnter.AddListener(() =>_towerSpawner.gameObject.SetActive(true));
        OnStateExit.AddListener(() => _towerSpawner.gameObject.SetActive(false));
    }

    protected override void Update()
    {
        if (_towerSpawner.IsBuilding)
        {
            Tile tile = _towerSpawner.UpdateTowerPosition();

            if (Input.GetKeyDown(KeyCode.Escape))
                _towerSpawner.CancelBuild();
            
            if (!Input.GetMouseButtonDown(0)) return;
            if (tile == null) return;
            
            if (_resourceHandler.CanAffort(_towerToBuild.BuildCost) && tile.Buildable)
            {
                _towerSpawner.PlaceTower(tile);
                _resourceHandler.AddGold(-_towerToBuild.BuildCost);
            }
            else if (!_resourceHandler.CanAffort(_towerToBuild.BuildCost))
            {
                Debug.Log("Not Enough Gold");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            _towerToBuild = _towerSpawner.SelectTower(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _towerToBuild = _towerSpawner.SelectTower(1);
    }
}