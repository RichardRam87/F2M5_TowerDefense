using System;
using UnityEngine;

public class TowerUpgrades<T> : ScriptableObject
{
    [SerializeField] private TowerUpgrade<T>[] _upgrades;
    public TowerUpgrade<T>[] Upgrades => _upgrades;
}

[Serializable]
public class TowerUpgrade<T>
{
    [SerializeField] private float _cost;
    public float Cost => _cost;

    [SerializeField] private T _upgradedStats;
    public T UpgradedStats => _upgradedStats;
}
