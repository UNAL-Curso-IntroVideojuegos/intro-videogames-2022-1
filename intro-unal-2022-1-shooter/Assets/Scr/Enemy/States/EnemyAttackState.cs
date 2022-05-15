using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float _attackDelta = 0;

    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");
        _attackDelta = 0;
    }
    
    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        _attackDelta += Time.deltaTime;
        if (_attackDelta > agent.AgentConfig.AttackDuration)
        {
            //agent.Animator.SetTrigger("Attack");
            //_attackDelta = 0;
            _attackDelta -= Time.deltaTime;
            if (_attackDelta <= 0){
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            }
        }
    }

}
