using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private IEnemyState _enemyStateImplementation;
    private float time= 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
        time = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        time += Time.deltaTime;
        if (time > agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
