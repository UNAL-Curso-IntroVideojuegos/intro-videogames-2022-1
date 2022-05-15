using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{

    private Vector3 punto_siguiente;
    private int _indiceActual = 0;
    private Vector3 GetAgentNextPoint(EnemyAgent agent)
    {
        
        if (agent.AgentConfig.PathPoints == null || agent.AgentConfig.PathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        _indiceActual = (_indiceActual + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[_indiceActual].position;
    }
    
    public void OnEnter(EnemyAgent agent)
    {
        punto_siguiente = GetAgentNextPoint(agent);
        agent.PathFindingController.GoTo(punto_siguiente ,callback:null);

    }

    public void OnUpdate(EnemyAgent agent)
    {

        float distancia = (punto_siguiente - agent.transform.position).magnitude;

        if (distancia <0.1f)
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
        agent.PathFindingController.Stop();
    }
}