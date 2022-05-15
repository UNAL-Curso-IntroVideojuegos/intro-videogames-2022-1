using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float _navMeshRefreshTimer = 0;

    private int _currentPointIndex = 0;

    private Vector3 GetAgentNextPoint(EnemyAgent agent)
    {
        //If we don't have a Path return (0, 0, 0)
        if (agent.AgentConfig.PathPoints == null || agent.AgentConfig.PathPoints.Count == 0)
        {
            return Vector3.zero;
        }

        //Move through the path. When reach the end -> start from 0 again
        _currentPointIndex = (_currentPointIndex + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[_currentPointIndex].position;
    }

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        _navMeshRefreshTimer = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        _navMeshRefreshTimer -= Time.deltaTime;

        //Pasaron X segundos, volver a Patrol
        if (_navMeshRefreshTimer <= 0)
        {
            _navMeshRefreshTimer = 5;
            agent.PathFindingController.GoTo(GetAgentNextPoint(agent), null);
            
        }

        //Reto: Punto3
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
        agent.PathFindingController.Stop();
    }
}
