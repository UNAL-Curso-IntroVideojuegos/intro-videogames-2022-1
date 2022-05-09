using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
    }
    private float timeIdle = 0;
    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        timeIdle += Time.deltaTime;
        if (agent.AgentConfig.IdleTime <= timeIdle)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
        timeIdle = 0;
    }
}
