using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float timer = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        timer = timer + Time.deltaTime;
        if(agent.AgentConfig.IdleTime < timer){
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }

        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        timer = 0;
        Debug.Log("Idle: OnExit");
    }
}
