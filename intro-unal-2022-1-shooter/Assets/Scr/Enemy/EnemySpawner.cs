using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyAgent> _enemies;
    
    void Start()
    {
        GameEvents.OnStartGameEvent += OnGameStart;
        GameEvents.OnGameOverEvent += OnGameOver;
        GameEvents.OnEnemyDeathEvent += OnEnemyDeath;

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].gameObject.SetActive(false);
        }
    }
    
    void OnDestroy()
    {
        GameEvents.OnStartGameEvent -= OnGameStart;
        GameEvents.OnGameOverEvent -= OnGameOver;
        GameEvents.OnEnemyDeathEvent -= OnEnemyDeath;
    }

    private void OnGameStart()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Reset();
        }
    }
    
    private void OnGameOver()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].gameObject.SetActive(false);
        }
    }

    private void OnEnemyDeath(int point)
    {
        bool areEnemiesAlive = false;
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].IsDeath)
            {
                areEnemiesAlive = true;
                break;
            }
        }

        if (areEnemiesAlive == false)
        {
            GameManager.Instance.GameOver();
        }
    }
}
