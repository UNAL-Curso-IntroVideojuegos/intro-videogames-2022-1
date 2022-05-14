using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHalthBar : MonoBehaviour
{
    [SerializeField]
    private float _totalHealth = 100;
    [SerializeField]
    [Range(0,100)]
    private float _health = 100;
    
    [Header("2D")]
    public Transform _healthMask;
    
    //[Header("UI - Test")]
    //public RectTransform _healthBar;
    // Image _healthImage;

    private void Start()
    {
        _health = _totalHealth;
    }

    void Update()
    {
        //_healthBar.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up*2);
        //_healthImage.fillAmount = _health / _totalHealth;

        _healthMask.localScale = new Vector3(_health / _totalHealth, 1, 1);
    }
}
