using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    private float time;
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
        time = 0.0f;
    }
    
    public void OnUpdate(EnemyAgent agent)
    {
        agent.Rot();
        
        time += Time.deltaTime;
        if (time > agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }
}
