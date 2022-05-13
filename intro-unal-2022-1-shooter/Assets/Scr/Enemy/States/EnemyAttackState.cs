using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _navMeshRefreshTimer = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
        _navMeshRefreshTimer = 0;
    }
    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        _navMeshRefreshTimer += Time.deltaTime;
        if (_navMeshRefreshTimer >= agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }
}
