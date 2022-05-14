using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float time = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        time = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        else
        {
            time += Time.deltaTime;
            if (time >= agent.AgentConfig.IdleTime)
            {
                agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
            }
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
