using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
   private float time = 0.0f;
   public void OnEnter(EnemyAgent agent)
   {
      Debug.Log("Attack: OnEnter");
      time = 0.0f;
      agent.Animator.SetTrigger("Attack");
      
   }

   public void OnUpdate(EnemyAgent agent)
   {
      
      Debug.Log("Attack: OnUpdate");

      
      time +=Time.deltaTime;
      
      if (agent.AgentConfig.AttackDuration < time)
      {
         agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
      }
   }

   public void OnExit(EnemyAgent agent)
   {
      Debug.Log("Attack: OnExit");
   }
}
