using System;
using UnityEngine;

public class WaveState : GameplayState
{
    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private float _spawnDelay;
    
    private void Awake()
    {   
        OnStateEnter.AddListener(() => _waveSystem.gameObject.SetActive(true));
        OnStateEnter.AddListener(() => _waveSystem.StartNextWave(_spawnDelay));
        
        OnStateExit.AddListener(() => _waveSystem.gameObject.SetActive(false));
    }
}
