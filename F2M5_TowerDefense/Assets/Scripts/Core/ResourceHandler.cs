using System;
using UnityEngine;
using UnityEngine.Events;

public class ResourceHandler : MonoBehaviour
{
    [Serializable]
    public class OnResourceGained : UnityEvent<int> { }

    [SerializeField] private int _startGold;
    [SerializeField] private OnResourceGained _onResourceGained;
    
    private int _gold;
    public int Gold => _gold;

    private void Start()
    {
        _gold = 0;
        AddGold(_startGold);
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        _onResourceGained?.Invoke(_gold);
    }

    public bool CanAffort(int cost)
    {
        return _gold >= cost;
    }
}
