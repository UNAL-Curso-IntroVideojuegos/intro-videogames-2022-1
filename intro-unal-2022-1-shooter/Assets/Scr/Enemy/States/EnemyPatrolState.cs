using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float _stayOnPointTimer;
    private int _currentPointIndex = 0;
    private Vector3 _nextPoint;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        _stayOnPointTimer = 0;
        _nextPoint = GetAgentNextPoint(agent);
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        
        if (agent.IsAtPatrolPoint(_nextPoint) && _stayOnPointTimer == 0)
        {
            _nextPoint = GetAgentNextPoint(agent);
            _stayOnPointTimer = agent.AgentConfig.StayOnPatrolPointTime;
        }
        else
        {
            if (_stayOnPointTimer <= 0)
            {
                _stayOnPointTimer = 0;
                agent.PathFindingController.GoTo(_nextPoint, null);
            }
            else
            {
                _stayOnPointTimer -= Time.deltaTime;
            }
        }
        
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
