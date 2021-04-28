using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _worthOnDeath = 1;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        SetupEnemy();
    }

    private void SetupEnemy()
    {
        ResourceHandler rh = GameObject.FindObjectOfType<ResourceHandler>();
        _health.OnDeath.AddListener(() => rh.AddGold(_worthOnDeath));
    }

    public Health GetHealth()
    {
        return _health;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
