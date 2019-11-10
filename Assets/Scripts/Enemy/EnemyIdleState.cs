using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
     private float _range = 20f;



    public override void DoAction()
    {
        if(ScanForPlayer())
        {
            _enemy.SetState(_enemy.gameObject.AddComponent<EnemyAttackPlayerState>());
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

    private bool ScanForPlayer()
    {
        return Vector3.Distance(_enemy.player.transform.position, transform.position) < _range;
    }

}
