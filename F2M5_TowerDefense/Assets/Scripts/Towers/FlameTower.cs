using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : Tower
{
    protected override bool CanAttack()
    {
        return (targetInRangeChecker.GetFirstEnemyInRange() != null);
    }

    protected override void Attack()
    {
        Debug.Log("Vlammen maar!");
    }
}
