using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFX : MonoBehaviour {

    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private AudioSource _gunAudio;
    [SerializeField] private GameObject  _impactPrefab;
    private ParticleSystem impactEffect;

    public void Initialize()
    {
        impactEffect = Instantiate(_impactPrefab).GetComponent<ParticleSystem>();
        _muzzleFlash = GetComponent<ParticleSystem>();
        _gunAudio = GetComponent<AudioSource>();
    }

    public void PlayShotEffects()
    {
        _muzzleFlash.Stop(true);
        _muzzleFlash.Play(true);
        _gunAudio.Stop();
        _gunAudio.Play();
    }

    public void PlayImpactEffect(Vector3 impactPosition)
    {
        impactEffect.transform.position = impactPosition;
        impactEffect.Stop();
        impactEffect.Play();
    }

    
}
