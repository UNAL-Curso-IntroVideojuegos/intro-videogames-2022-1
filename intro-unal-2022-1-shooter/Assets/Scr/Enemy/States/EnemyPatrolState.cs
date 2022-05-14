using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private Vector3 nextPoint;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        if (!agent.AgentConfig.Patrolling)
        {
            nextPoint = GetAgentNextPoint(agent);
            agent.AgentConfig.Patrolling = true;
        }
        
        agent.PathFindingController.GoTo(nextPoint, null);

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        if ((agent.transform.position - nextPoint).magnitude <= 1)
        {
            Debug.Log("To idle");
            agent.AgentConfig.Patrolling = false;
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
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
        agent.AgentConfig.CurrentPoint = (agent.AgentConfig.CurrentPoint + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[agent.AgentConfig.CurrentPoint].position;
    }
}

