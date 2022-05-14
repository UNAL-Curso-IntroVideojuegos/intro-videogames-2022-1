using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    private float timeAux = 0;
    private float timeAux2 = 0;
    private float timeAuxAtack = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Atack: OnEnter");
        //se ve mejor sin activarlo en el onEnter
        //agent.Animator.SetTrigger("Attack");
        timeAux2 = Time.time;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Atack: OnUpdate");
        //Mirar al jugador
        agent.transform.LookAt(agent.Target.position);
        float distanceToTarget = (agent.Target.position - agent.transform.position).magnitude;
        //atacara si esta lo sugficientemente cerca
        if (distanceToTarget < agent.AgentConfig.AttackRange)
        {
            //aqui se calcula el tiempo de cada atake siempre y cuando este a rango
            if (timeAux < timeAux2+agent.AgentConfig.FightTime){
                if (Time.time > timeAuxAtack){
                    agent.Animator.SetTrigger("Attack");
                    timeAuxAtack = Time.time + agent.AgentConfig.AttackDuration;
                }
                timeAux = Time.time;
            }
            //Despues de cumplir los X segundos de pelea volvera a Idel 
            else{
                agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
            }

            
            
            
        }
        //en caso de no estar en el rango de ataque cambiara a chase
        else{
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        
        
        
    
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
