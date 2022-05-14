using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float timer = 0;
    
    public void OnEnter(EnemyAgent agent){
        agent.Animator.SetTrigger("Attack");
    }
    
    public void OnUpdate(EnemyAgent agent){
        timer = timer + Time.deltaTime;
        if(agent.AgentConfig.AttackDuration < timer){
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

     public void OnExit(EnemyAgent agent){
        timer = 0;
    }
   
}
