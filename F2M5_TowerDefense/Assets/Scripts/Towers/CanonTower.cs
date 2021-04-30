using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _shootSpeed = 1f;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _gunObject;
    
    private Enemy _target;


    protected override bool CanAttack()
    {
        _target = GetFirstEnemyInRange();
        return _target != null;
    }

    protected override void Attack()
    {
        RotateTowardsTarget();

        if (timer >= _shootSpeed)
        {
            _target.GetHealth().TakeDamage(0);
            timer = 0;
            Debug.Log("Kanonnen los!");
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = _target.transform.position - _gunObject.position;
        float singleStep = _rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(_gunObject.forward, targetDirection, singleStep, 0f);
        _gunObject.rotation = Quaternion.LookRotation(newDirection);
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
