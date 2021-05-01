using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanonTowerStats", menuName = "TowerStats/CanonTowerStats", order = 1)]
public class CanonTowerStats : ScriptableObject
{
    [SerializeField] private float _radius;
    public float Radius => _radius;
    
    [SerializeField] private float _shootSpeed = 1f;
    public float ShootSpeed => _shootSpeed;

    [SerializeField] private float _damage;
    public float Damage => _damage;
    
    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _shootAngle;
    public float ShootAngle => _shootAngle;
}
