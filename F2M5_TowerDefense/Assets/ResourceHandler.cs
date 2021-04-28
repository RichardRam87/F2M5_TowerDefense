using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceHandler : MonoBehaviour
{
    [Serializable]
    public class OnResourceGained : UnityEvent<int> { }

    [SerializeField] private OnResourceGained _onResourceGained;
    
    private int _gold;

    private void Start()
    {
        _gold = 0;
        AddGold(0);
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        _onResourceGained?.Invoke(_gold);
    }
}
