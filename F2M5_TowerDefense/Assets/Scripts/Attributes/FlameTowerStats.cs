using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FlameTowerStats", menuName = "TowerStats/FlameTowerStats", order = 1)]
public class FlameTowerStats : TowerStats<FlameTowerData>
{
    
}

[Serializable]
public class FlameTowerData
{
    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _damagePerTick;
    public float DamagePerTick => _damagePerTick;

    [SerializeField] private float _ticksPerSecond;
    public float TicksPerSecond => _ticksPerSecond;
    
    [SerializeField] private float _dotDuration;
    public float DotDuration => _dotDuration;
}


