using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float staying = 0f;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        staying = 0;
        Debug.Log(staying);
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        if (staying>=agent.AgentConfig.IdleTime)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
        staying = staying + Time.deltaTime;
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
