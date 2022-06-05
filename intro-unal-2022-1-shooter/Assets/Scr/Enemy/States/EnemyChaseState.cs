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
            if (distanceToTarget > agent.AgentConfig.AttackRange)
            {
                Transform target = agent.Target;
                agent.PathFindingController.GoTo(target.position, null);
            }

            _navMeshRefreshTimer = agent.AgentConfig.PathfindingRefreshTime;
        }

        if (distanceToTarget <= agent.AgentConfig.AttackRange)
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
