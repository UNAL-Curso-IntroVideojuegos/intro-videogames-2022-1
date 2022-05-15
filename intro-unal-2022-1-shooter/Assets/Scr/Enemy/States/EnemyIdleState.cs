using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _navMeshRefreshTimer = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        _navMeshRefreshTimer = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        _navMeshRefreshTimer -= Time.deltaTime;

        //Pasaron X segundos, volver a Patrol
        if (_navMeshRefreshTimer <= 0)
        {
            _navMeshRefreshTimer = agent.AgentConfig.IdleTime;
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
        Debug.Log("Idle: OnExit");
    }
}
