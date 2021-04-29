using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Animation _animation;
    private bool _isSetup;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
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
            _animation.Play();
        }
        _isSetup = true;
    }
}
