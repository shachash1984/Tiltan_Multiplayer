using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Weapon weapon;
    private EnemyState _state;
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        _state.DoAction();
    }

    public void SetState(EnemyState newState)
    {
        _state = newState;
        _state.SetEnemy(this);
    }
}
