using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _navMeshRefreshTimer = 0;
    private float _timer;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        _navMeshRefreshTimer = 0;
        _timer =agent.AgentConfig.AttackDuration;
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");

        _timer -= Time.deltaTime;

        /*
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
        }*/

        if (_timer <= 0)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
        agent.PathFindingController.Stop();
    }
}
