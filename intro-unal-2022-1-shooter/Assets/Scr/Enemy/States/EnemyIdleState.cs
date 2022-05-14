using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float timeAux = 0;
    private float timeAux2 = Time.time;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        timeAux = 0;
        timeAux2 = Time.time;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        

        //Quite el if de ver al player, ya que si esta en pelea y vuele a Idle entrara de nuevo a chase porque el jugador esta en su campo de vision
        //por lo cual la pelea no terminara, hay que tener encuenta que en el estado de chase no vera al jugador, solo en Patrol
        //Vi al Player -> Pasar al estado Chase! 

        /**if (agent.IsLookingTarget())
        //{
            //agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }**/

        if (timeAux < timeAux2+agent.AgentConfig.IdleTime){
                
                timeAux = Time.time;
            }
        else{
            
            agent.AgentConfig.IdleTime = agent.AgentConfig.PathfindingRefreshTime;
            
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }


    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
        
    }
}
