using System;
using UnityEngine;

public class BuildState : GameplayState
{
    [SerializeField] private TowerSpawner _towerSpawner;

    private void Awake()
    {
        OnStateEnter.AddListener(() =>_towerSpawner.gameObject.SetActive(true));
        OnStateExit.AddListener(() => _towerSpawner.gameObject.SetActive(false));
    }
}