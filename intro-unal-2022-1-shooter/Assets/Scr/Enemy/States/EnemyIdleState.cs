using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float toPatrol;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        toPatrol = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        toPatrol += Time.deltaTime;
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        if (toPatrol > agent.AgentConfig.IdleTime)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
