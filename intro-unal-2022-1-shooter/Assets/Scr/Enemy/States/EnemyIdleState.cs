using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    float patrolTimer;
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        patrolTimer = 0;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        patrolTimer += Time.deltaTime;
        Debug.Log("Idle: OnUpdate");
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        if (patrolTimer > 3)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
