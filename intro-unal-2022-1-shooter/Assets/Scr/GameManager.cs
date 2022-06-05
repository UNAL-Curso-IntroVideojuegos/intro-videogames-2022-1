using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private StaticCamera _camera;

    public Transform Player => _player;
    public StaticCamera Camera => _camera;
    
    private int _score = 0;
    private int _level = 0;
    private float _time  = 0;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        AudioManager.Instance.Init();
        
        ReturnToMenu();
        
        GameEvents.OnEnemyDeathEvent += OnEnemyDeath;
        GameEvents.OnBonusCollectedEvent += OnBonusCollected;
        GameEvents.OnLevelProgressEvent += OnLevelProgress;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDeathEvent -= OnEnemyDeath;
        GameEvents.OnBonusCollectedEvent -= OnBonusCollected;
        GameEvents.OnLevelProgressEvent -= OnLevelProgress;
    }
    
    public void ReturnToMenu()
    {
        GameEvents.OnStartScreenEvent?.Invoke();
        _player.gameObject.SetActive(false);
        
        AudioManager.Instance.PlayMusic(AudioMusicType.Menu);
    }
    
    public void StartGame()
    {
        _score = 0;
        _time = Time.time;
        GameEvents.OnStartGameEvent?.Invoke();
        
        _player.GetComponent<LivingEntity>().Reset();
        GameEvents.OnScoreUpdated?.Invoke(_score);
        
        AudioManager.Instance.PlayMusic(AudioMusicType.Gameplay);
    }

    [ContextMenu("GameOver")]
    public void GameOver()
    {
        _time = Time.time - _time;
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        if (_score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _score);
        }
        
        _player.gameObject.SetActive(false);
        GameEvents.OnGameOverEvent?.Invoke(_score, _score > maxScore, _time, _level);
        
        AudioManager.Instance.PlayMusic(AudioMusicType.End);


        //TODO: Not ideal - Need optmization
        DropItem[] drops = FindObjectsOfType<DropItem>();
        for (int i = drops.Length - 1; i >= 0; i--)
        {
            Destroy(drops[i].gameObject);
        }
    }
    
    void OnEnemyDeath(EnemyAgent _, int enemyPoints)
    {
        _score += enemyPoints;
        GameEvents.OnScoreUpdated?.Invoke(_score);
    }

    void OnBonusCollected(int points)
    {
        _score += points;
        GameEvents.OnScoreUpdated?.Invoke(_score);
    }

    void OnLevelProgress(int level)
    {
        _level = level + 1;
    }
}
