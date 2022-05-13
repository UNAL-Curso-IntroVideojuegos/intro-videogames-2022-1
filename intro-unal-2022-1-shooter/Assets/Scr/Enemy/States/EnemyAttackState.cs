using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    float attackTimer = 0;
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("Attack");

    }

    public void OnExit(EnemyAgent agent)
    {
        
    }

    public void OnUpdate(EnemyAgent agent)
    {
        attackTimer += Time.deltaTime;

        agent.transform.LookAt(agent.Target.transform);

        float targetRange = (agent.Target.position - agent.transform.position).magnitude;
        bool isInRange = targetRange <= agent.AgentConfig.AttackRange;
        if (targetRange> agent.AgentConfig.AttackRange)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }

        if (isInRange && attackTimer > agent.AgentConfig.AttackDuration)
        {
            agent.Animator.SetTrigger("Attack");
            attackTimer = 0;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
