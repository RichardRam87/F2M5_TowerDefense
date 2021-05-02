using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowersUI : MonoBehaviour
{
    [SerializeField] private AnimationClip _fadeAnimation;
    [SerializeField] private AnimationClip _reverseFadeAnimation;
    
    private Animation _animation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    public void PlayFadeAnimation()
    {
        _animation.clip = _fadeAnimation;
        _animation.Play();
    }

    public void PlayReverseFadeAnimation()
    {
        _animation.clip = _reverseFadeAnimation;
        _animation.Play();
    }
}
