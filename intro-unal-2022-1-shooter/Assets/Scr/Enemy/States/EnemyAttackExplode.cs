using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackExplode : IEnemyState
{
    private const float HIDE_TIMER = 0.25f;
    
    private float _hideTimer = HIDE_TIMER;
    
    public void OnEnter(EnemyAgent agent)
    {
        _hideTimer = HIDE_TIMER;
        agent.Animator.SetTrigger("Explode");
    }

    public void OnUpdate(EnemyAgent agent)
    {
        _hideTimer -= Time.deltaTime;
        if (_hideTimer <= 0)
        {
            Explode(agent);
            
            GameEvents.OnEnemyDeathEvent?.Invoke(agent, 0);
            
            agent.gameObject.SetActive(false);
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
    }

    private void Explode(EnemyAgent agent)
    {
        Collider[] hits = Physics.OverlapSphere(agent.transform.position, agent.AgentConfig.AttackRange, LayerMask.GetMask("Player"));
        for (int i = 0; i < hits.Length; i++)
        {
            if (agent.Target.TryGetComponent(out LivingEntity entity))
            {
                entity.TakeDamage(agent.AgentConfig.AttackDamage);
            }
        }

        if (agent.AgentConfig.ExplodeVFX != null)
        {
            GameObject vfx = GameObject.Instantiate(agent.AgentConfig.ExplodeVFX, agent.transform.position + Vector3.up * 1.3f, Quaternion.identity);
            GameObject.Destroy(vfx, 4);
        }
    }
}
