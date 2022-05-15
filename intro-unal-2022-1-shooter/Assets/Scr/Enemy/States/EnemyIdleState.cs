using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float timeidle = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        timeidle += Time.deltaTime;
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            timeidle = 0;
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        if (timeidle > agent.AgentConfig.IdleTime)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
            timeidle = 0;
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
