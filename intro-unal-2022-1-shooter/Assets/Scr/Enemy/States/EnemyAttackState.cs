using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
    }
    private float timeIdle=0;
    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        timeIdle += Time.deltaTime;
        if (agent.AgentConfig.AttackDuration <= timeIdle)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
        timeIdle = 0;
    }
}
