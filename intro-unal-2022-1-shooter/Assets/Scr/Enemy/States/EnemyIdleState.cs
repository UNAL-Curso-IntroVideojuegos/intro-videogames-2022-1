using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    float timeidle;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        timeidle = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");

        timeidle = timeidle + Time.deltaTime;
        //Debug.Log(timeidle);

        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        if (timeidle > agent.AgentConfig.IdleTime)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }

    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
        timeidle = 0;
    }
}
