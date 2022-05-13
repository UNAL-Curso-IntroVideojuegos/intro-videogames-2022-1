using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float auxiliarTime;

    public void OnEnter(EnemyAgent agent)
    {
        //Iniciamos la animación
        agent.Animator.SetTrigger("Attack");
    
    }

    public void OnUpdate(EnemyAgent agent)
    {
        auxiliarTime += Time.deltaTime; //Aumentamos nuestro contador de tiempo

        // Cuando se cumplan "AttackDuration" segundos se vuelve a iniciar el ataque
        if (auxiliarTime >= agent.AgentConfig.AttackDuration)
        {
            agent.Animator.SetTrigger("Attack");
            auxiliarTime = 0; //Reiniciamos el contador
        }

        // Revisamos si está dentro del rango de ataque
        if ((agent.Target.position - agent.transform.position).magnitude > agent.AgentConfig.AttackRange)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent){    }
}