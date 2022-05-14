using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    //Declaring a Class Variable for the timer to control the transition from
    //the Idle state to the Patrol state
    private float _idleToPatrolTimer = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        
        //Setting the timer to zero for each time the Idle state begins
        _idleToPatrolTimer = 0;       
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        //Accumulating time
        _idleToPatrolTimer += Time.deltaTime;
        
        //Defining a limit for the transition from the Idle state to the Patrol state
        if (_idleToPatrolTimer > agent.AgentConfig.IdleTime)
        {
            //Changing to the Patrol state
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }   
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
