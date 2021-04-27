using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startHealth;

    [SerializeField] private UnityEvent OnTakeDamage;
    [SerializeField] private UnityEvent OnDeath;
    
    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float dmg = 1)
    {
        _currentHealth = Mathf.Max(_currentHealth - dmg, 0);
        OnTakeDamage?.Invoke();

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public float GetNormalizedHealth()
    {
        return _currentHealth / _startHealth; 
    }
}
