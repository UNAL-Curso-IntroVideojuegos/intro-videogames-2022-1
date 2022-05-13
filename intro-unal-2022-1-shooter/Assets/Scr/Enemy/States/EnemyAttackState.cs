using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    float timeatt = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");

        
        timeatt = timeatt + Time.deltaTime;

        if (timeatt > agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle); 
        }

        
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
        timeatt = 0;
    }
}