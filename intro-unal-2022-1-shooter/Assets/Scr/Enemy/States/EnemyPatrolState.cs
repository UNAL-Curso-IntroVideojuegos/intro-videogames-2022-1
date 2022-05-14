using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float NewPoint = 0.1f;
    private int ActualPoint = 0;

    private float timeAux = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");

    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");

        Vector3 distance = agent.AgentConfig.PathPoints[ActualPoint].position - agent.transform.position;

        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        else if (distance.magnitude < NewPoint){

            ActualPoint ++;
            if(ActualPoint>= 3){
                ActualPoint = 0;

            }
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        else {
            agent.PathFindingController.GoTo(agent.AgentConfig.PathPoints[ActualPoint].position, null);
        }

    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
    }
    
}