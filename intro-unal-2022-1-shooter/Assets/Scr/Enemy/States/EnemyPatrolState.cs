using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    float proximity = 0;
    Vector3 nextPosition;
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
        nextPosition = GetAgentNextPoint(agent);
    }

    public void OnExit(EnemyAgent agent)
    {

    }

    public void OnUpdate(EnemyAgent agent)
    {
        agent.PathFindingController.GoTo(nextPosition, null);
        proximity = (nextPosition - agent.transform.position).magnitude;
        if (proximity < 0.1) nextPosition = GetAgentNextPoint(agent);

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }
}
