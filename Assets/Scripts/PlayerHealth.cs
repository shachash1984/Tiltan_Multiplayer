using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SerializeField] private float _maxHealth = 100;
    [SyncVar (hook = "OnHealthChanged")] private float _health;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    [ServerCallback]
    private void OnEnable()
    {
        _health = _maxHealth;
    }

    [Server]
    public bool TakeDamge(float dmg)
    {
        bool died = false;
        if (_health <= 0)
            return died;

        _health -= dmg;
        died = _health <= 0f;

        RpcTakeDamage(died);
        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (isLocalPlayer)
            PlayerCanvas.S.FlashDamageEffect();
        if (died)
            _player.Die();
    }

    private void OnHealthChanged(float value)
    {
        _health = value;
        if (isLocalPlayer)
            PlayerCanvas.S.SetHealth(value);
    }

    public float GetHealth()
    {
        return _health;
    }
}
