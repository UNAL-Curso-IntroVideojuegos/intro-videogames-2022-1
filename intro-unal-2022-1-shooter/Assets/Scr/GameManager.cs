using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform _player;

    public Transform Player => _player;

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
        _player.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        GameEvents.OnStartScreenEvent?.Invoke();
        _player.gameObject.SetActive(false);
    }
    
    public void StartGame()
    {
        GameEvents.OnStartGameEvent?.Invoke();
        _player.GetComponent<LivingEntity>().Reset();
    }

    [ContextMenu("GameOver")]
    public void GameOver()
    {
        _player.gameObject.SetActive(false);
        GameEvents.OnGameOverEvent?.Invoke();
    }
}
