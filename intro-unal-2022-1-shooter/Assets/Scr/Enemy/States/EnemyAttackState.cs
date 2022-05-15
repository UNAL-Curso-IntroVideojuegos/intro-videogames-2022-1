using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{


    public float _time = 0;
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
        _time = 0;

    }

    public void OnUpdate(EnemyAgent agent)
    {
        _time += Time.deltaTime;

        if (_time >= agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }

    }

    public void OnExit(EnemyAgent agent)
    {

    }
}
