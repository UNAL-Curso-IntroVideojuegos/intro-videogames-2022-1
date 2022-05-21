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
            agent.gameObject.SetActive(false);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }
}
