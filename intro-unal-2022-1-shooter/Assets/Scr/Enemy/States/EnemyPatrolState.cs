using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private int _currentPointIndex = 0;
    Vector3 target;

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
        Debug.Log("Patrol: OnUpdate");

        float distanceToTarget = (target - agent.transform.position).magnitude;
        
        // Definir nuevo objetivo si ya llegué al anterior
        if(distanceToTarget < 1)
        {
            target = GetAgentNextPoint(agent);
        }
        
        agent.PathFindingController.GoTo(target, null);

        // Si patrullando ve al jugador -> Perseguir
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
