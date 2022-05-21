using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _timer = 0;

    public void OnEnter(EnemyAgent agent)
    {
        _timer = agent.AgentConfig.AttackDuration;
        agent.Animator.SetTrigger("Attack");
        
        if (agent.Target.TryGetComponent(out LivingEntity entity))
        {
            entity.TakeDamage(agent.AgentConfig.AttackDamage);
        }
    }

    public void OnUpdate(EnemyAgent agent)
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }

        //Look at player
        Vector3 targetDir = (agent.Target.position - agent.transform.position);
        agent.transform.rotation =
            Quaternion.RotateTowards(agent.transform.rotation, Quaternion.LookRotation(targetDir), 5f);
    }

    public void OnExit(EnemyAgent agent)
    {
    }
}