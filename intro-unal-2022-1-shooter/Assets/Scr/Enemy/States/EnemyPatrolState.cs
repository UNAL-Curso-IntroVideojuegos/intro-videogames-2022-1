using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    public void OnEnter(EnemyAgent agent){
        Debug.Log("Patrol: OnEnter");
        Vector3 pos = GetAgentNextPoint(agent);
        agent.PathFindingController.GoTo(pos, null);
    }
    public void OnUpdate(EnemyAgent agent){
        Debug.Log("Patrol: OnUpdate");
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        if (agent.PathFindingController.IsStopped){
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }        
    }
    public void OnExit(EnemyAgent agent){
        Debug.Log("Patrol: OnExit");
        agent.PathFindingController.Stop();
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
}
