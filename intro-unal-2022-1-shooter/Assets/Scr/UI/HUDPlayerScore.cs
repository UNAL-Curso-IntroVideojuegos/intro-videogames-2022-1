using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPlayerScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        SetScore(0);
        GameEvents.OnScoreUpdated += SetScore;
    }

    private void OnDestroy()
    {
        GameEvents.OnScoreUpdated -= SetScore;
    }
    
    void SetScore(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }
}
