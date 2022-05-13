using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _punchTimer = 0;
    public void OnEnter(EnemyAgent agent){
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
        _punchTimer = agent.AgentConfig.AttackDuration;
    }
    public void OnUpdate(EnemyAgent agent){
        Debug.Log("Attack: OnUpdate");
        _punchTimer -= Time.deltaTime;
        if (_punchTimer <= 0){
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }
    public void OnExit(EnemyAgent agent){
        Debug.Log("Attack: OnExit");
    }
}
