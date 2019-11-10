using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour {

    protected EnemyAI _enemy;
    public abstract void DoAction();
    public void SetEnemy(EnemyAI eai)
    {
        _enemy = eai;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    
}
