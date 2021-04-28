using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    [SerializeField]
    private float _animationSpeed = 0.5f;

    [SerializeField] 
    private float _dissapearDelay = 1f;
    
    [SerializeField]
    private Text _gamestateText;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowGameStateUI(string text)
    {
        gameObject.SetActive(true);
        StartCoroutine(GameStateUIRoutine(text));
    }

    private IEnumerator GameStateUIRoutine(string text)
    {
        _gamestateText.text = text;
        gameObject.SetActive(true);
        _animator.speed = 1f / _animationSpeed;
        yield return new WaitForSeconds(_dissapearDelay + _animationSpeed);

        gameObject.SetActive(false);
    }
    
}
