using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    float attacking = 0f;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        Debug.Log(agent.AgentConfig.AttackDuration);
        //Vi al Player -> Pasar al estado Chase! 
        if (attacking>=agent.AgentConfig.AttackDuration)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        attacking = attacking + Time.deltaTime;
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
