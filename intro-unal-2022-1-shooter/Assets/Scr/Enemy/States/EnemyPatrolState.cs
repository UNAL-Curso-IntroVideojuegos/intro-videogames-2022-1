using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    
    private Vector3 nextpoint;
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
       // Debug.Log("Patrol: OnEnter");
        Debug.Log(_currentPointIndex);
        Debug.Log(message:"El contador es "+ agent.AgentConfig.PathPoints.Count);
        nextpoint = GetAgentNextPoint(agent);
        agent.PathFindingController.GoTo(nextpoint ,callback:null);
        
    }

    public void OnUpdate(EnemyAgent agent)
    {
      //  Debug.Log("Patrol: OnUpdate");
        
        float Distance = (nextpoint - agent.transform.position).magnitude;
        
        if (Distance <0.1f)
        {
            Debug.Log(message:"Condi1");
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
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
}
