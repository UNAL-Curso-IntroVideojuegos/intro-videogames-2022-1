using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    // Start is called before the first frame update
    private float chagePoint = 0.1f;
    private int currentPoint = 0;

    private float timeAux = 0;
     public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        
        Vector3 distance = agent.AgentConfig.PathPoints[currentPoint].position - agent.transform.position;

         if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        else if (distance.magnitude < chagePoint){
            
            currentPoint ++;
            if(currentPoint>= 2){
                currentPoint = 0;

            }
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
         
     

        }
        else {
            agent.PathFindingController.GoTo(agent.AgentConfig.PathPoints[currentPoint].position, null);
            
        }
        

        
    
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
    }

    
}
