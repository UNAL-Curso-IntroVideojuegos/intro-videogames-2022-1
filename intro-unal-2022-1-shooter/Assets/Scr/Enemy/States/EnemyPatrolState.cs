using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    bool Call;
    Vector3 next;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        Call = true;
        next = GetAgentNextPoint(agent);
        Debug.Log(_currentPointIndex);
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");

        //agent.StateMachineController.ChangeToState(EnemyStateType.Idle);

        if (Call)
        {
            Call = false;
            Debug.Log(next);
        }

        float distanceToNext = (next - agent.transform.position).magnitude;
        agent.PathFindingController.GoTo(next, null);

        if (distanceToNext<0.1)
        {
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
        Debug.Log(next);
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

