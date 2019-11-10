using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour {

    [SerializeField] private float _fadeSpeed = 0.1f;
    [SerializeField] private CanvasGroup _groupToFade;
    [SerializeField] private bool _startVisible;
    [SerializeField] private bool _startWithFade;

    private bool _visible;

    private void Reset()
    {
        _groupToFade = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (_startVisible)
            SetVisible();
        else
            SetInvisible();

        if (!_startWithFade)
            return;

        if (_visible)
            StartFadeOut();
        else
            StartFadeIn();
    }

    private void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_groupToFade.alpha < 1f)
        {
            _groupToFade.alpha += _fadeSpeed * Time.deltaTime;
            yield return null;
        }
        _visible = true;
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (_groupToFade.alpha > 0f)
        {
            _groupToFade.alpha -= _fadeSpeed * Time.deltaTime;
            yield return null;
        }
        _visible = false;
    }

    private void SetVisible()
    {
        _groupToFade.alpha = 1f;
        _visible = true;
    }

    private void SetInvisible()
    {
        _groupToFade.alpha = 0f;
        _visible = false;
    }

    internal void Flash()
    {
        StartCoroutine(ProcessFlash());
    }

    private IEnumerator ProcessFlash()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(FadeOut());
    }
}
