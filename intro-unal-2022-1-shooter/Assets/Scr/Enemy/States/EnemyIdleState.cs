using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _moveToPatrolAt = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        _moveToPatrolAt = Time.time + agent.AgentConfig.IdleTime;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        //PLayer in range 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        
        //After X sec -> To patrol
        if (Time.time > _moveToPatrolAt)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
            return;
        }
    }

    public void OnExit(EnemyAgent agent)
    {
    }
}
