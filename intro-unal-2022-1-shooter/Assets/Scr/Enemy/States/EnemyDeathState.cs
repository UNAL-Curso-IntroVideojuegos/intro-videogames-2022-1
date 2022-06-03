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
            TryDrop(agent.AgentConfig.NumberOfDrops, agent.AgentConfig.DropItems, spawnPoint);
            agent.gameObject.SetActive(false);
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }

    private void TryDrop(int numberOfDrops, EnemyDropItem[] dropItems, Vector3 spawnPoint)
    {
        if (numberOfDrops <= 0 || dropItems == null || dropItems.Length == 0)
        {
            return;
        }

        float totalProbability = 0;
        for (int i = 0; i < dropItems.Length; i++)
        {
            totalProbability += dropItems[i].Probability;
        }
        
        for (int i = 0; i < numberOfDrops; i++)
        {
            float scaledRandomValue = Random.value * totalProbability;
            int selectedIndex = 0;
            float probabilitySum = 0f;
            while (selectedIndex < dropItems.Length)
            {
                probabilitySum += dropItems[selectedIndex].Probability;
                if (scaledRandomValue < probabilitySum)
                {
                    break;
                }
                selectedIndex++;
            }

            Vector3 randomPos = Random.insideUnitCircle * 3;
            randomPos.y = 0;
            MonoBehaviour.Instantiate(dropItems[selectedIndex].ItemPrefab, spawnPoint + randomPos, Quaternion.Euler(0, Random.value * 360, 0));
        }
    }
}
