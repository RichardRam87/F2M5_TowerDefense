using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private float _shootSpeed = 1f;
    
    private Enemy _target;
    
    protected override bool CanAttack()
    {
        _target = targetInRangeChecker.GetFirstEnemyInRange();
        return _target != null && timer >= _shootSpeed;
    }

    protected override void Attack()
    {
        _target.GetHealth().TakeDamage(1);
        timer = 0;
        Debug.Log("Kanonnen los!");
    }
}
