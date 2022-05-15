using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState

{
    private float tiempoRefresco = 0;
   
    public void OnEnter(EnemyAgent agente)
    {
        agente.Animator.SetTrigger("Attack");
        tiempoRefresco = 0;        
    }

    
    public void OnUpdate(EnemyAgent agente)
    {
        tiempoRefresco += Time.deltaTime;

        if (agente.AgentConfig.AttackDuration <= tiempoRefresco){
            agente.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        
    }

    public void OnExit (EnemyAgent agente){
        // Saliendo
    }
}
