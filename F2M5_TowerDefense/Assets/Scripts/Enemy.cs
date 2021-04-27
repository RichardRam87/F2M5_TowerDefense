using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
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
