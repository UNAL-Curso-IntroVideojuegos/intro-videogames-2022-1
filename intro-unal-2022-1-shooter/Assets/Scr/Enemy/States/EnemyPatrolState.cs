using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        for (int i = agent.AgentConfig.PathPoints.Count - 1; i >= 0; i--)
        {
            if (agent.AgentConfig.PathPoints[i] == null)
            {
                agent.AgentConfig.PathPoints.RemoveAt(i);
            }
        }
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
            return GetRandomPoint(agent);
        }
        
        _currentPointIndex = (_currentPointIndex + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[_currentPointIndex].position;
    }

    private Vector3 GetRandomPoint(EnemyAgent agent)
    {
        float walkRadius = 3;
        Vector3 randomDirection = Random.onUnitSphere * walkRadius;
        randomDirection.y = 0;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        return hit.position;
    }
}
