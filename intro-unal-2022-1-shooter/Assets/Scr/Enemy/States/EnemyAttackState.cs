using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    //Declaring a Class Variable for the timer to control the Attack state duration
    private float _attackTimer = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Attack: OnEnter");
        
        //Setting the timer to zero for each time the Attack state begins
        _attackTimer = 0;
        
        //Getting the attack animation from the Animator tool
        agent.Animator.SetTrigger("Attack");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Attack: OnUpdate");
        
        //Accumulating time
        _attackTimer += Time.deltaTime;
        
        //Defining a limit for the Attack state
        if (_attackTimer > agent.AgentConfig.AttackDuration)
        {
            //Changing to the Idle state
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Attack: OnExit");
    }
}
