using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _shootSpeed = 1f;
    
    private Enemy _target;
    
    protected override bool CanAttack()
    {
        _target = GetFirstEnemyInRange();
        return _target != null && timer >= _shootSpeed;
    }

    protected override void Attack()
    {
        _target.GetHealth().TakeDamage(1);
        timer = 0;
        Debug.Log("Kanonnen los!");
    }
    
    private Enemy GetFirstEnemyInRange()
    {
        Enemy enemy = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _layer);

        foreach (Collider hitCollider in hitColliders)
        {
            Enemy e = hitCollider.transform.GetComponent<Enemy>();
            if (e != null)
            {
                enemy = e;
                break;
            }
        }
        return enemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
