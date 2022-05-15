using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attackTimer = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        attackTimer = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        attackTimer += Time.deltaTime;
        float distanceToTarget = (agent.Target.position - agent.transform.position).magnitude;
        
        if (attackTimer > agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }

        if (distanceToTarget < agent.AgentConfig.AttackRange)
        {
            agent.Animator.SetTrigger("Attack");
        }
        
        if (distanceToTarget > agent.AgentConfig.AttackRange)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
