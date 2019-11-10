using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererToggler : MonoBehaviour {

    [SerializeField] private float _turnOnDelay = 0.1f;
    [SerializeField] private float _turnOffDelay = 3.5f;
    [SerializeField] private bool _enabledOnLoad = false;
    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>(true);

        if (_enabledOnLoad)
            EnableRenderers();
        else
            DisableRenderers();
    }

    public void ToggleRenderersDelayed(bool isOn)
    {
        if (isOn)
            Invoke("EnableRenderers", _turnOffDelay);
        else
            Invoke("DisableRenderers", _turnOffDelay);
    }

    private void DisableRenderers()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].enabled = false;
        }
    }

    private void EnableRenderers()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].enabled = true;
        }
    }

    public void ChangeColor(Color newColor)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = newColor;
        }
    }
}
