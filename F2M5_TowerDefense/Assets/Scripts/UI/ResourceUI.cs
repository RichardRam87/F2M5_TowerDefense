using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Animator _animator;
    private bool _isSetup;
    private readonly int _resourceGained = Animator.StringToHash("ResourceGained");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _isSetup = false;
    }

    public void UpdateUI(int amount)
    {
        _text.text = "Gold: " + amount;

        if (_isSetup)
        {
            _animator.SetTrigger(_resourceGained);
        }
        _isSetup = true;
    }
}
