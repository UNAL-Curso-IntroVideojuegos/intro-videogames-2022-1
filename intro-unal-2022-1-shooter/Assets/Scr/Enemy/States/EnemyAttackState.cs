using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    float attackCooldown = 0;
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
    }

    public void OnExit(EnemyAgent agent)
    {

    }

    public void OnUpdate(EnemyAgent agent)
    {
        attackCooldown += Time.deltaTime;
        if (attackCooldown > agent.AgentConfig.AttackDuration)
        {
            agent.Animator.SetTrigger("Attack");
            attackCooldown = 0;
        }
    }
}
