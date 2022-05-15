using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    
    private float _navMeshRefreshTimer = 0;

    private int _currentPointIndex = 0;
    private Vector3 NewPosition = Vector3.zero;
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
        _navMeshRefreshTimer = 0;
        NewPosition = GetAgentNextPoint(agent);

    }

    public void OnUpdate(EnemyAgent agent)
    {
        _navMeshRefreshTimer -= Time.deltaTime;
        if (_navMeshRefreshTimer <= 0)
        {
            agent.PathFindingController.GoTo(NewPosition,null);
        }

        

        float distanceToTarget = (NewPosition - agent.transform.position).magnitude;
        if (distanceToTarget <= 0.08)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }

        _navMeshRefreshTimer = agent.AgentConfig.PathfindingRefreshTime;
        Debug.Log(distanceToTarget);

    }

    public void OnExit(EnemyAgent agent)
    {
       agent.PathFindingController.Stop();
    }

}
