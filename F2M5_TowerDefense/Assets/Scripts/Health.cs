using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startHealth;

    [Serializable]
    public class OnTakeDamage : UnityEvent<float> { }

    [SerializeField] private OnTakeDamage _OnTakeDamage;
    [SerializeField] private UnityEvent _OnDeath;
    public UnityEvent OnDeath => _OnDeath;

    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float dmg = 1)
    {
        _currentHealth = Mathf.Max(_currentHealth - dmg, 0);
        _OnTakeDamage?.Invoke(dmg);

        if (_currentHealth <= 0)
        {
            _OnDeath?.Invoke();
        }
    }

    public float GetNormalizedHealth()
    {
        return _currentHealth / _startHealth; 
    }
}
