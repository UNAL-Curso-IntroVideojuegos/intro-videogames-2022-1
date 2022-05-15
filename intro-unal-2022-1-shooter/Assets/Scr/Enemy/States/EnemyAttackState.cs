using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _attackTimer;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");

        agent.transform.LookAt(agent.Target.position);
        agent.Animator.SetTrigger("Attack");
        _attackTimer = agent.AgentConfig.AttackDuration;

    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        _attackTimer -= Time.deltaTime;
        
        if (_attackTimer <= 0)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
