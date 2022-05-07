using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    private Vector3 _currentPoint;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");

    }
    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        
        float distanceToPoint = Vector3.Distance(_currentPoint,  agent.transform.position);
        
        // If enemy hasn't reached the point, he continues moving 
        if (distanceToPoint > 0.2)
        {
            agent.PathFindingController.GoTo(_currentPoint, null);
        }
        else
        {
            _currentPoint = GetAgentNextPoint(agent);
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        
        //Vi al Player -> Pasar al estado Chase! 
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
