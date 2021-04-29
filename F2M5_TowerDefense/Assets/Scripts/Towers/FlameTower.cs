using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : Tower
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layer;

    [SerializeField] private float _tickDamage;
    [SerializeField] private float _ticksPerSecond;
    [SerializeField] private float _dotDuration;
    
    [SerializeField] private FlamePoint[] _flamePoints;

    private Dictionary<FlamePoint, List<Enemy>> _validEnemyMap;

    protected override void Awake()
    {
        base.Awake();
        
        _validEnemyMap = new Dictionary<FlamePoint, List<Enemy>>();

        foreach (FlamePoint flamePoint in _flamePoints)
        {
            _validEnemyMap.Add(flamePoint, new List<Enemy>());
        }
    }
    
    // TODO: Clearing each enemylist is inefficient, refactor
    protected override bool CanAttack()
    {
        foreach (FlamePoint flamePoint in _flamePoints)
        {
            Bounds b = CalculateFlamePointBounds(flamePoint, _range);
            Collider[] cols = Physics.OverlapBox(b.center, b.extents, Quaternion.identity, _layer);
            
            _validEnemyMap[flamePoint].Clear();
            
            foreach (Collider col in cols)
            {
                Enemy e = col.transform.GetComponent<Enemy>();
                if (e.GetHealth().HasActiveDOT) continue;
                
                _validEnemyMap[flamePoint].Add(e);
            }
            if (cols.Length > 0) return true;
        }
        return false; 
    }
    
    protected override void Attack()
    {
        foreach (FlamePoint flamePoint in _flamePoints)
        {
            if (!(_validEnemyMap[flamePoint].Count > 0)) continue;
             
            foreach (Enemy enemy in _validEnemyMap[flamePoint])
            {
                enemy.GetHealth().AddDamageOverTimeEffect(_tickDamage, _ticksPerSecond, _dotDuration);
            }
        }
    }

    private Bounds CalculateFlamePointBounds(FlamePoint flamePoint, float range)
    {
        Vector3 centerPosition = flamePoint.Position + (flamePoint.Forward * range) / 2f;
        Vector3 areaOfEffectScale = Vector3.one;

        if (Mathf.Abs(flamePoint.Forward.x) > 0)
        {
            areaOfEffectScale = new Vector3(_range, 1, 1);
        }
        else if (Mathf.Abs(flamePoint.Forward.z) > 0)
        {
            areaOfEffectScale = new Vector3(1, 1, _range);
        }
        return new Bounds(centerPosition, areaOfEffectScale);
    }

    private void OnDrawGizmos()
    {
        foreach (FlamePoint flamePoint in _flamePoints)
        {
            Gizmos.color = Color.red;
            Bounds b = CalculateFlamePointBounds(flamePoint, _range);
            
            Gizmos.DrawWireCube(b.center, b.size);
        }
    }
}
