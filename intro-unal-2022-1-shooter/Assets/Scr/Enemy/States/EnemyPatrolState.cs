using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    Vector3 target;
    private int _currentPointIndex = 0;
    private float waittime = 0;
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
        Debug.Log("Patrol: OnEnter");
        target = GetAgentNextPoint(agent);
    }

    public void OnUpdate(EnemyAgent agent)
    {   
        if ((target - agent.transform.position).magnitude < 0.5)
        {
            waittime += Time.deltaTime; 
        }
        if (waittime > agent.AgentConfig.IdleTime)
        {
            waittime = 0;
            target = GetAgentNextPoint(agent);
        }
        agent.PathFindingController.GoTo(target, null);
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
