using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPlayerState : EnemyState
{
    private Weapon _weapon;
    private Ray ray;


    public override void DoAction()
    {
        if (_enemy.player)
            Attack();
        else
        {
            _enemy.SetState(_enemy.gameObject.AddComponent<EnemyIdleState>());
            Destroy(this);
        }
            
    }

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    private void Attack()
    {
        ray = new Ray(transform.position, _enemy.player.transform.position - transform.position);
        _weapon.Attack(ray);
    }



}
