using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _attackTime;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
        _attackTime = agent.AgentConfig.AttackDuration;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        _attackTime -= Time.deltaTime;

        float distanceToTarget = (agent.Target.position - agent.transform.position).magnitude;

        // Si se aleja del rango, dejar de atacar
        if (distanceToTarget > agent.AgentConfig.AttackRange)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        // Atacar cada X segundos
        if (_attackTime < 0)
        {
            agent.Animator.SetTrigger("Attack");
            _attackTime = agent.AgentConfig.AttackDuration;
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
