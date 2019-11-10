using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCanvas : MonoBehaviour {

    public static PlayerCanvas S;
    [Header("Component References")]
    [SerializeField] private Image _reticle;
    [SerializeField] private UIFader _damageImage;
    [SerializeField] private Text _gameStatusText;
    [SerializeField] private Text _healthValue;
    [SerializeField] private Text _killsValue;
    [SerializeField] private Text _logText;
    [SerializeField] private AudioSource _deathAudio;

    private void Awake()
    {
        if (S != null)
            Destroy(gameObject);
        S = this;
    }

    //Called only in editor
    private void Reset()
    {

        _reticle = GameObject.Find("Reticle").GetComponent<Image>();
        _damageImage = GameObject.Find("DamageFlash").GetComponent<UIFader>();
        _gameStatusText = GameObject.Find("GameStatusText").GetComponent<Text>();
        _healthValue = GameObject.Find("HealthText").GetComponent<Text>();
        _killsValue = GameObject.Find("KillsText").GetComponent<Text>();
        _logText = GameObject.Find("LogText").GetComponent<Text>();
        _deathAudio = GameObject.Find("DeathAudio").GetComponent<AudioSource>();
    }

    public void Initialize()
    {
        _reticle.enabled = true;
        _gameStatusText.text = "";
        SetHealth(FindObjectOfType<PlayerHealth>().GetHealth());
    }

    public void HideReticle()
    {
        _reticle.enabled = false;
    }

    public void FlashDamageEffect()
    {
        _damageImage.Flash();
    }

    public void PlayDeathAudio()
    {
        if (!_deathAudio.isPlaying)
            _deathAudio.Play();
    }

    public void SetKills(int amount)
    {
        _killsValue.text = string.Format("Kills: {0}", amount.ToString());
    }

    public void SetHealth(float amount)
    {
        _healthValue.text = string.Format("Health: {0}", amount.ToString());
    }

    public void WriteLogText(string text, float duration)
    {
        CancelInvoke();
        _logText.text = text;
        Invoke("ClearLogText", duration);
    }

    private void ClearLogText()
    {
        _logText.text = "";
    }

    public void WriteGameStatusText(string text)
    {
        _gameStatusText.text = text;
    }
}
