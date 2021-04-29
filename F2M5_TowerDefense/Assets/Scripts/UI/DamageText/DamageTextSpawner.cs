using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] private DamageText _damageTextPrefab;
    
    public void Spawn(float damageAmount)
    {
        DamageText instance = Instantiate(_damageTextPrefab, transform);
        instance.SetText(damageAmount);
    }
}
