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

            Vector3 spawnPoint = agent.AgentConfig.DeathVFXPoint.position;
            spawnPoint.y = 0;
            TryDrop(agent.AgentConfig.DropItems, spawnPoint);
            agent.gameObject.SetActive(false);
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }

    private void TryDrop( EnemyDropItem[] dropItems, Vector3 spawnPoint)
    {
        if (dropItems == null || dropItems.Length == 0)
        {
            return;
        }

        for (int i = 0; i < dropItems.Length; i++)
        {
            if (Random.value <= dropItems[i].Probability)
            {
                Vector3 randomPos = Random.insideUnitCircle * 3;
                randomPos.y = 0;
                MonoBehaviour.Instantiate(dropItems[i].ItemPrefab, spawnPoint + randomPos, Quaternion.Euler(0, Random.value * 360, 0));
            }
        }
    }
}
