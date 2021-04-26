using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSystem : MonoBehaviour
{
    [Serializable]
    public struct WaveData
    {
        public Enemy[] enemies;
        public float interval;
    }

    [SerializeField] private WaveData[] _waves;
    [SerializeField] private UnityEvent OnWaveComplete;

    private int _waveIndex = 0;
    private float _startDelay;
    private List<Enemy> _activeEnemies;
    private bool _isWaveActive = false;

    public void StartNextWave(float startDelay = 0)
    {
        _startDelay = startDelay;
        StartWave(_waves[_waveIndex]);
    }

    private void StartWave(WaveData waveData)
    {
        StartCoroutine(WaveUpdate(waveData));
    }

    public bool LastWaveFinished()
    {
        return _waveIndex >= _waves.Length;
    }

    IEnumerator WaveUpdate(WaveData waveData)
    {
        bool waveFinishedSpawning = false;
        bool waveCompleted = false;
        float timer = 0;
        int enemyIndex = 0;
        
        List<Enemy> activeEnemies = new List<Enemy>();
        _isWaveActive = true;

        yield return new WaitForSeconds(_startDelay);
        
        while (!waveFinishedSpawning || !waveCompleted)
        {
            timer += Time.deltaTime;

            if (timer >= waveData.interval && enemyIndex < waveData.enemies.Length)
            {
                Enemy enemy = waveData.enemies[enemyIndex];
                Instantiate(enemy, transform);
                enemyIndex++;
                timer = 0;
                activeEnemies.Add(enemy);
                
                if (enemyIndex >= waveData.enemies.Length)
                {
                    waveFinishedSpawning = true;
                    Debug.Log("Wave Spawning Complete");
                }
            }
            
            // check if we've completed the wave 
            // for now check if we still have enemies in game
            // todo: convert this to a check in the activeenemies list
            Enemy[] e = FindObjectsOfType<Enemy>();
            if (enemyIndex >= waveData.enemies.Length && waveFinishedSpawning && e.Length == 0)
            {
                Debug.Log("Wave Completed!");
                
                waveCompleted = true;
            }
            yield return null;
        }
        _isWaveActive = false;
        _waveIndex++;
        
        // this call should be made by the gamestate...
        if (LastWaveFinished())
        {
            Debug.Log("Game Completed!");
        }
        OnWaveComplete?.Invoke();
    }
}
