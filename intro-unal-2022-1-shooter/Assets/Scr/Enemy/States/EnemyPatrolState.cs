using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    private float _navMeshRefreshTimer = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");_navMeshRefreshTimer -= Time.deltaTime;

        float distanceToTarget = (agent.AgentConfig.PathPoints[_currentPointIndex].position - agent.transform.position).magnitude;
        
        //Persiga al player
        if (_navMeshRefreshTimer <= 0)
        {
                Transform target = agent.AgentConfig.PathPoints[_currentPointIndex];
                agent.PathFindingController.GoTo(target.position, null);

                _navMeshRefreshTimer = agent.AgentConfig.PathfindingRefreshTime;
        }

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        if (distanceToTarget <= 0.1)
        {
            GetAgentNextPoint(agent);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
    }

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
}
