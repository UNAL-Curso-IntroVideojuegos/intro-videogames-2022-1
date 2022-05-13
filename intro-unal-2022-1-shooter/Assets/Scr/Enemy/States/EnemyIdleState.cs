using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{

    private float _timer;


    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        _timer = agent.AgentConfig.IdleTime;
    }

    public void OnUpdate(EnemyAgent agent)
    {

        Debug.Log("Idle: OnUpdate");
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        _timer -= Time.deltaTime;

        if(_timer<= 0){
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }

    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
