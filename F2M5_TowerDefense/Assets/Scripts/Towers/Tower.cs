using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetInRangeChecker))]
public abstract class Tower : MonoBehaviour
{
    protected TargetInRangeChecker targetInRangeChecker;

    private void Awake()
    {
        targetInRangeChecker = GetComponent<TargetInRangeChecker>();
    }
    
    void Update()
    {
        if (!CanAttack()) return;

        Attack();
    }

    protected abstract bool CanAttack();
    protected abstract void Attack();
}
