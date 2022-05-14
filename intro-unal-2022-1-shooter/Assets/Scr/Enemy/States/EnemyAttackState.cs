using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    float attackDelta = 0;
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
    }

    public void OnExit(EnemyAgent agent)
    {

    }

    public void OnUpdate(EnemyAgent agent)
    {
        attackDelta += Time.deltaTime;
        if (attackDelta > agent.AgentConfig.AttackDuration)
        {
            agent.Animator.SetTrigger("Attack");
            attackDelta = 0;
        }
    }
}