using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    float _currentAttackTime = 0;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Atack: OnEnter");
        
        agent.Animator.SetTrigger("Attack");
        _currentAttackTime = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        _currentAttackTime += Time.deltaTime;
        lookAtPlayer(agent);
        
        if (_currentAttackTime > agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }

    private void lookAtPlayer(EnemyAgent agent)
    {
        Vector3 targetRelativeAngle = agent.Target.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(targetRelativeAngle);
    }
}
