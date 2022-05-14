using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
	private float time;
	
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Idle: OnEnter");
		time = 0.0f;
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Idle: OnUpdate, Time: " + time);
        time += Time.deltaTime;

        // Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
		// Llevo mucho tiempo en estado Idle -> Pasar al estado Patrol!
		else if (time > agent.AgentConfig.IdleTime) 
		{
			agent.StateMachineController.ChangeToState(EnemyStateType.Patrol);
		}
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Idle: OnExit");
    }
}
