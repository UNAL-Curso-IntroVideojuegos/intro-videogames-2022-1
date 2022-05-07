using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float _timeToPatrol;

    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
        _timeToPatrol = agent.AgentConfig.TimeToPatrol;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate");

        _timeToPatrol -= Time.deltaTime;

        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }

        // Pasar a Patrol luego de cierto tiempo
        if (_timeToPatrol < 0)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
