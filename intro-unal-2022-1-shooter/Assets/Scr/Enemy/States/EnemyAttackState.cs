using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _attackTime;
    private float time = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        _attackTime = agent.AgentConfig.AttackDuration;
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        time += Time.deltaTime;
        Debug.Log("Attack: OnUpdate");
        if (time > _attackTime){
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            time = 0;
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
