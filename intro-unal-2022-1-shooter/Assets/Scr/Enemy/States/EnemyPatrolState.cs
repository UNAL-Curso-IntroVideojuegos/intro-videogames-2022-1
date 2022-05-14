using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{

    private int _currentPointIndex = 0;
    private Vector3 _nextPoint;

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

    public void OnEnter(EnemyAgent agent){
        _nextPoint = GetAgentNextPoint(agent);
    }

    public void OnUpdate(EnemyAgent agent){
        if(!agent.IsLookingTarget()){
            agent.PathFindingController.GoTo(_nextPoint, null);
            if((_nextPoint - agent.transform.position).magnitude < 0.1f){
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            }
        }else{
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent){
        agent.PathFindingController.Stop();
    }

}
