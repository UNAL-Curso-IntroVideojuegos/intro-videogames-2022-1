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
        GameEvents.OnBonusCollectedEvent += OnEnemyDeath;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDeathEvent -= OnEnemyDeath;
        GameEvents.OnBonusCollectedEvent -= OnEnemyDeath;
    }

    void OnEnemyDeath(int enemyPoints)
    {
        score += enemyPoints;
        SetScore(score);
    }

    void SetScore(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }
}
