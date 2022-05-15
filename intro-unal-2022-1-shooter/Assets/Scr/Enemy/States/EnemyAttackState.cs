using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSate : IEnemyState
{
    private float time = 0;
    public void OnEnter(EnemyAgent agent)
    {
    Debug.Log("Attack: OnEnter");
    }
    public void OnUpdate(EnemyAgent agent)
    {
        if (time == 0)
        {
            agent.Animator.SetTrigger("Attack");
        }
        time += Time.deltaTime;
        if (time > agent.AgentConfig.AttackDuration)
        {
            if ((agent.Target.position - agent.transform.position).magnitude > agent.AgentConfig.AttackRange)
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
            time = 0;
        }
    }
    public void OnExit(EnemyAgent agent)
    {
    Debug.Log("Attack: OnExit");
    }

}
