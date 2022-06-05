using System;
using TMPro;
using UnityEngine;

public class HUDEndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _leveText;
    [SerializeField]
    private GameObject _newMaxScore;


    public void SetResults(int score, bool isMaxScore, float time, int level)
    {
        _scoreText.text = $"Score: {score}";
        _newMaxScore.SetActive(isMaxScore);
        
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timeText.text = $"Time: {timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

        _leveText.text = $"Level: {level}";
    }
}
