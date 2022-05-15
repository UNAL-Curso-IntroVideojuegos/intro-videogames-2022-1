using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{

    private float _navMeshRefreshTimer = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Chase: OnEnter");
        _navMeshRefreshTimer = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Chase: OnUpdate");

        _navMeshRefreshTimer -= Time.deltaTime;

        float distanceToTarget = (agent.Target.position - agent.transform.position).magnitude;
        
        //Persiga al player
        if (_navMeshRefreshTimer <= 0)
        {   
            //Mejora para que no lo persiga casi todo el rato
            //Lo persigue si la distancia entre los dos está AgentConfig.AttackRange y 5
            if (distanceToTarget > agent.AgentConfig.AttackRange && distanceToTarget <= 5)
            {
                Transform target = agent.Target;
                agent.PathFindingController.GoTo(target.position, null);
            }
            //Si la distancia entre los dos es mayor a 5, vuelva a Idle
            else if (distanceToTarget > 5)
            {
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            }

            _navMeshRefreshTimer = agent.AgentConfig.PathfindingRefreshTime;
        }

        if (distanceToTarget < agent.AgentConfig.AttackRange)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Attack);
        }
        
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Chase: OnExit");
        agent.PathFindingController.Stop();
    }
}
