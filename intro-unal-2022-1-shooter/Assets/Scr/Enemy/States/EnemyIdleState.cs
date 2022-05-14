using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _IdleTime;
    private float time = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        _IdleTime = agent.AgentConfig.IdleTime;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");

        time += Time.deltaTime;
        if (time > _IdleTime)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
            time = 0;
        }
        
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }
    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}