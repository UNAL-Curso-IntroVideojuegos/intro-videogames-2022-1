using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyWave[] _enemyWaves;
    [SerializeField]
    private Vector2 _spawnArea;

    private bool _shouldSpawn = false;
    private int _currentWaveIndex;
    private int _pendingEnemiesToSpawn;
    private int _spawnCountThisWave;
    private float _initialDelay = 0;
    private float _timeToNextSpawn = 0; 
    private List<EnemyAgent> _spawnedAliveEnemies = new List<EnemyAgent>();
    
    void Start()
    {
        GameEvents.OnStartGameEvent += OnGameStart;
        GameEvents.OnGameOverEvent += OnGameOver;
        GameEvents.OnEnemyDeathEvent += OnEnemyDeath;
    }
    
    void OnDestroy()
    {
        GameEvents.OnStartGameEvent -= OnGameStart;
        GameEvents.OnGameOverEvent -= OnGameOver;
        GameEvents.OnEnemyDeathEvent -= OnEnemyDeath;
    }

    private void Update()
    {
        if (!_shouldSpawn || _currentWaveIndex >= _enemyWaves.Length)
        {
            return;
        }
        
        bool canSpawn = false;
        //Go to next way?
        if (_currentWaveIndex < 0 ||
            (_spawnCountThisWave >= _enemyWaves[_currentWaveIndex].NumberOfSpawns && _pendingEnemiesToSpawn <= 0 &&
             _spawnedAliveEnemies.Count == 0))
        {
            _currentWaveIndex++;
            _spawnCountThisWave = 0;
            _pendingEnemiesToSpawn = 0;
            if (_currentWaveIndex < _enemyWaves.Length)
            {
                _initialDelay = _enemyWaves[_currentWaveIndex].DelayToSpawn;
                // EventDispatcher.Instance.Dispatch(new StartWaveEvent
                // {
                //     waveNumber = currentWaveIndex + 1,
                //     isBoos = enemyWaves[currentWaveIndex].wordType == WordGroupType.Boss,
                //     isInfinite = enemyWaves[currentWaveIndex].isInfinite
                // });
                canSpawn = true;
            }
            else
            {
                //EventDispatcher.Instance.Dispatch(new EndGameEvent { playerWon = true });
                GameManager.Instance.GameOver();
            }
        }
        else if (_spawnCountThisWave < _enemyWaves[_currentWaveIndex].NumberOfSpawns)
        {
            canSpawn = true;
        }
        
        if (_initialDelay > 0)
        {
            _initialDelay -= Time.deltaTime;
            canSpawn = false;
        }
        
        // Spawn next enemy?
        if (canSpawn && _timeToNextSpawn <= 0 && _currentWaveIndex < _enemyWaves.Length)
        {
            SpawnEnemy();
            _timeToNextSpawn = _enemyWaves[_currentWaveIndex].TimeBetweenSpawns;
        }

        _timeToNextSpawn -= Time.deltaTime;
    }

    private void OnGameStart()
    {
        _shouldSpawn = true;
        _currentWaveIndex = 0;
        _spawnCountThisWave = 0;
        _pendingEnemiesToSpawn = 0;
        _timeToNextSpawn = 0;
    }
    
    private void OnGameOver()
    {
        StopAllCoroutines();
        _shouldSpawn = false;
        for (int i = 0; i < _spawnedAliveEnemies.Count; i++)
        {
            Destroy(_spawnedAliveEnemies[i].gameObject);
        }
        _spawnedAliveEnemies.Clear();
    }

    private void OnEnemyDeath(EnemyAgent deathEnemy, int point)
    {
        _spawnedAliveEnemies.Remove(deathEnemy);
    }
    
    private void SpawnEnemy()
    {
        EnemyWave wave = _enemyWaves[_currentWaveIndex];
        
        // Choose which enemy to spawn
        float totalProbability = 0;
        for (int i = 0; i < wave.EnemiesItems.Length; i++)
        {
            totalProbability += wave.EnemiesItems[i].Probability;
        }

        float scaledRandomValue = Random.value * totalProbability;
        int selectedIndex = 0;
        float probabilitySum = 0f;
        while (selectedIndex < wave.EnemiesItems.Length)
        {
            probabilitySum += wave.EnemiesItems[selectedIndex].Probability;
            if (scaledRandomValue < probabilitySum)
            {
                break;
            }
            selectedIndex++;
        }
        
        //Get enemy prefab
        EnemyAgent enemyPrefab = wave.EnemiesItems[selectedIndex].EnemyPrefab;
        //Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        Vector3 randomPoint = new Vector3(Random.Range(-_spawnArea.x, _spawnArea.x), 0,
            Random.Range(-_spawnArea.y, _spawnArea.y));
        NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 3, 1);
        Vector3 spawnPoint = hit.position; 
        
        _spawnCountThisWave++;
        StartCoroutine(SpawnEnemyWithDelay(enemyPrefab, spawnPoint));
    }

    IEnumerator SpawnEnemyWithDelay(EnemyAgent enemyPrefab, Vector3 spawnPoint)
    {
        _pendingEnemiesToSpawn++;
        
        //Get indicator prefab
        yield return new WaitForSeconds(1);
        EnemyAgent newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.Euler(0, Random.value * 360, 0));

        _pendingEnemiesToSpawn--;
        _spawnedAliveEnemies.Add(newEnemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 size = new Vector3(_spawnArea.x*2, 0.1f, _spawnArea.y*2);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
