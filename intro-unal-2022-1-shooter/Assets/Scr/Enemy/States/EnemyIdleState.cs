using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    float iddleCooldown = 0;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");

        iddleCooldown += Time.deltaTime;
        if (iddleCooldown > agent.AgentConfig.IdleTime)
        {
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
