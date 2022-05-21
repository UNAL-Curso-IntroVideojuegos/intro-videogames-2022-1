using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
    }

    public void OnUpdate(EnemyAgent agent)
    {
        if (agent.PathFindingController.IsStopped)
        {
            agent.PathFindingController.GoTo(GetAgentNextPoint(agent), () =>
            {
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            });
        }

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }
    
    public void OnExit(EnemyAgent agent)
    {
        agent.PathFindingController.Stop();
    }
    
    
    private Vector3 GetAgentNextPoint(EnemyAgent agent)
    {
        if (agent.AgentConfig.PathPoints == null || agent.AgentConfig.PathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        
        _currentPointIndex = (_currentPointIndex + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[_currentPointIndex].position;
    }
}
