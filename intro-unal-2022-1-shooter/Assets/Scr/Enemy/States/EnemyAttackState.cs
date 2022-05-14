using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attackTime = 0;

    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
        attackTime = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        attackTime -= Time.deltaTime;

        if (attackTime <= 0)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}