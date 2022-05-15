using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float waiting = 0f;
    private float _navMeshRefreshTimer = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        _navMeshRefreshTimer = 0;
        waiting = 0;
    }

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

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        Vector3 newPoint = GetAgentNextPoint(agent);

        _navMeshRefreshTimer -= Time.deltaTime;
        
        if (_navMeshRefreshTimer <= 0)
        {
            if (agent.transform.position != newPoint)
            {
                agent.PathFindingController.GoTo(newPoint, null);
            }

            _navMeshRefreshTimer = agent.AgentConfig.PathfindingRefreshTime;
        } 
        

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
    }
}
