using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _idleTimer = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        _idleTimer = 0;
        _idleTimer = agent.AgentConfig.IdleTime;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        _idleTimer -= Time.deltaTime;
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        if (_idleTimer <= 0){
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
