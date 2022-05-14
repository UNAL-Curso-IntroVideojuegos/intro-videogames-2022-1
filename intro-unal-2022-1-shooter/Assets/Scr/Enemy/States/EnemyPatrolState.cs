using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    private Vector3 nextPoint;
    private float distanceToNextPoint;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        nextPoint = GetAgentNextPoint(agent);
        agent.PathFindingController.GoTo(nextPoint, null);
    }
    
    public void OnUpdate(EnemyAgent agent)
    {
        
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        else
        {
            distanceToNextPoint = (nextPoint - agent.transform.position).magnitude;
            Debug.Log("Patrol: OnUpdate, Index: " + _currentPointIndex + ", Distance: " + distanceToNextPoint);
            if (distanceToNextPoint <= 0.5f)
            {
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            }
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