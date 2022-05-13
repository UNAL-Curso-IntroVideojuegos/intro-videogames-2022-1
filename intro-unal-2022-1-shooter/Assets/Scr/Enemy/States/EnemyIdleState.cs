using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float timeForPatrol;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        if (timeForPatrol >= agent.AgentConfig.IdleTime){
            timeForPatrol = 0;
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
        timeForPatrol += Time.deltaTime;
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
