using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPlayerScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int score = 0;
    
    private void Start()
    {
        SetScore(0);
        GameEvents.OnEnemyDeathEvent += OnEnemyDeath;
        GameEvents.OnBonusCollectedEvent += OnBonusCollected;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDeathEvent -= OnEnemyDeath;
        GameEvents.OnBonusCollectedEvent -= OnBonusCollected;
    }

    void OnEnemyDeath(EnemyAgent _, int enemyPoints)
    {
        score += enemyPoints;
        SetScore(score);
    }

    void OnBonusCollected(int points)
    {
        score += points;
        SetScore(score);
    }

    void SetScore(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }
}
