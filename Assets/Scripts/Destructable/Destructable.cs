using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Destructable : NetworkBehaviour {

    public delegate void DamageAction(float damage, Vector3 shotDirection);

    [SyncEvent] public event DamageAction EventOnTakeDamage;
    public event Action OnDestruction;


    [Range(0, 1000)] [SerializeField]
    private int maxHP = 100;
    [Range(0, 1000)] [SerializeField]
    private int currentHP = 100;
    
    public float destructionDelay = 3f;
    public float HPRatio
    {
        get { return (float)currentHP / maxHP; }
        private set { HPRatio = value; }
    }

    private void Start()
    {
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public bool TakeDamage(int dmg, Vector3 shotDirection)
    {
        if (dmg <= 0)
            return false;
        currentHP -= dmg;
        if (EventOnTakeDamage != null)
            EventOnTakeDamage(HPRatio, shotDirection);
        if(currentHP <= 0)
        {
            currentHP = 0;
            Destruction();
            return true;
        }
        return false;
    }

    public void Destruction()
    {
        if (OnDestruction != null)
            OnDestruction();
        if (gameObject)
            Destroy(gameObject, destructionDelay);
    }

    
}
