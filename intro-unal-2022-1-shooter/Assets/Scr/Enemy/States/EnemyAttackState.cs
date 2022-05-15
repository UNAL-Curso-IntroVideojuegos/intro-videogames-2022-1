using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attacking = 0f;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
        attacking = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        //Vi al Player -> Pasar al estado Chase! 
        if (attacking>=agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        attacking += Time.deltaTime;
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
