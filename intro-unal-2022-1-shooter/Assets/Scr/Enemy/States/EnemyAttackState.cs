using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    private float _navMeshRefreshTimer = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        _navMeshRefreshTimer = 0;
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        _navMeshRefreshTimer -= Time.deltaTime;

        //Pasaron X segundos, volver a Idle
        if (_navMeshRefreshTimer <= 0)
        {
            _navMeshRefreshTimer = agent.AgentConfig.AttackDuration;
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
