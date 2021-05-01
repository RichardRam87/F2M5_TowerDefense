using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private CanonTowerStats _stats;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _gunObject;
    [SerializeField] private CanonTowerUpgrades _upgrades;
    
    private Enemy _target;
    private CanonTowerData _towerData;
    private int _currentUpgradeIndex;

    protected override void Awake()
    {
        base.Awake();
        _towerData = _stats.Data;
    }

    protected override bool CanAttack()
    {
        _target = GetFirstEnemyInRange();

        if (_target == null) return false;
        
        Vector3 targetDirection = _target.transform.position - _gunObject.position;
        RotateTowardsTarget(targetDirection);
        float angle = GetAngleToTarget(targetDirection);

        return (timer >= _towerData.ShootSpeed && angle <= _towerData.ShootAngle);
    }

    protected override void Attack()
    {
        _target.GetHealth().TakeDamage(_towerData.Damage);
        timer = 0;
        Debug.Log("Kanonnen los!");
    }
    
    private float GetAngleToTarget(Vector3 direction)
    {
        return Vector3.Angle(direction, _gunObject.forward);
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        
        float singleStep = _towerData.RotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(_gunObject.forward, direction, singleStep, 0f);
        _gunObject.rotation = Quaternion.LookRotation(newDirection);
    }

    private Enemy GetFirstEnemyInRange()
    {
        Enemy enemy = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _towerData.Radius, _layer);

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
        Gizmos.DrawWireSphere(transform.position, _towerData.Radius);
    }
    
    public override bool CanUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyNextUpgrade()
    {
        // deze naamgeving is weird...
        _towerData = _upgrades.Upgrades[_currentUpgradeIndex].UpgradedStats;
        _currentUpgradeIndex++;
        print("Apply Upgrade");
    }
}
