using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float time = 0.0f;
    public void OnEnter(EnemyAgent agent)
    {
        time = 0.0f;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        time +=Time.deltaTime;
      
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        else if (agent.AgentConfig.IdleTime < time)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
    }
}
