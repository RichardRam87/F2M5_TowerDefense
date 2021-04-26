using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    private Enemy _target;
    protected override bool CanAttack()
    {
        _target = targetInRangeChecker.GetFirstEnemyInRange();

        return _target != null;
    }

    protected override void Attack()
    {
        Debug.Log("Kanonnen los!");
    }
}
