using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IEnemyState
{
    private float _hideTimer = 3f;
    
    public void OnEnter(EnemyAgent agent)
    {
        agent.Animator.SetTrigger("IsDeath");
        agent.Collider.enabled = false;
        _hideTimer = 3f;
        }

    public void OnUpdate(EnemyAgent agent)
    {
        _hideTimer -= Time.deltaTime;
        if (_hideTimer <= 0)
        {
            AudioManager.Instance.PlaySound2D("EnemyDeath");

            if (agent.AgentConfig.DeathVFX != null)
            {
                GameObject vfx = GameObject.Instantiate(agent.AgentConfig.DeathVFX, agent.AgentConfig.DeathVFXPoint.position, Quaternion.identity);
                GameObject.Destroy(vfx, 4);
            }

            agent.gameObject.SetActive(false);
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }
}
