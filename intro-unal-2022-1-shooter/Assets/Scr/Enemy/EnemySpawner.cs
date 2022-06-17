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

    private bool IsWaveSpawnCompleted => _spawnCountThisWave >= _enemyWaves[_currentWaveIndex].NumberOfSpawns;
    private bool AreEnemiesPendingToSpawn => _pendingEnemiesToSpawn > 0;
    private bool AreEnemiesAliveOnThisWave => _spawnedAliveEnemies.Count == 0;
    
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
        // - Edge condition: current index < 0
        // - If we already spawn all enemies on this way AND all enemies did died
        if (_currentWaveIndex < 0 ||
            (IsWaveSpawnCompleted && !AreEnemiesPendingToSpawn && AreEnemiesAliveOnThisWave))
        {
            _currentWaveIndex++;
            _spawnCountThisWave = 0;
            _pendingEnemiesToSpawn = 0;
            
            //Next wave
            if (_currentWaveIndex < _enemyWaves.Length)
            {
                _initialDelay = _enemyWaves[_currentWaveIndex].DelayToSpawn;
                canSpawn = true;
                GameEvents.OnLevelProgressEvent?.Invoke(_currentWaveIndex);
                
                AudioManager.Instance.PlaySound2D("LevelCompleted");
            }
            //In case we don't have more ways -> Game Ends
            else
            {
                GameManager.Instance.GameOver();
            }
        }
        else if (!IsWaveSpawnCompleted) //If we haven't spawn all the enemies on this way -> Continue spawnming
        {
            canSpawn = true;
        }
        
        //Delay after complete each wave
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
    
    private void OnGameOver(int score, bool isMaxScore, float time, int level)
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

        //If is infinite -> just don't count them
        if (!wave.IsInfinite)
        {
            _spawnCountThisWave++;
        }

        StartCoroutine(SpawnEnemyWithDelay(enemyPrefab, spawnPoint));
    }

    IEnumerator SpawnEnemyWithDelay(EnemyAgent enemyPrefab, Vector3 spawnPoint)
    {
        _pendingEnemiesToSpawn++;
        
        //TODO: Add indicator object to indicate where it's gonna spawn
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
