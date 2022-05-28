using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar_Shader : EnemyHalthBar
{
    [SerializeField]
    private SpriteRenderer _healthBarRenderer;
    private MaterialPropertyBlock _healthBarPropertyBlock;

    private int _currentHealth;
    private int _totalHealth;
    private float _damagedHealthRatio = 0;
    private float _lastDamagedAt = -Mathf.Infinity;

    private void Start()
    {
        _healthBarPropertyBlock = new MaterialPropertyBlock();
        _healthBarRenderer.GetPropertyBlock(_healthBarPropertyBlock);
        _healthBarPropertyBlock.SetFloat("_Alpha", 0);
        _healthBarRenderer.SetPropertyBlock(_healthBarPropertyBlock);
    }

    public override void UpdateHealthBar(float health, float totalHealth, int damage)
    {
        _currentHealth = (int) health;
        _totalHealth = (int) totalHealth;
        _damagedHealthRatio += (damage / totalHealth);
        _lastDamagedAt = Time.time;
    }
    
    public void Update()
    {
        float alpha = 1f - Mathf.Max(0, Mathf.Min(0.5f, Time.time - _lastDamagedAt - 0.5f) * 2f);

        if (alpha <= 0 && _healthBarPropertyBlock.GetFloat("_Alpha") <= 0) {
            return;
        }
        
        _healthBarPropertyBlock.SetFloat("_Value", _currentHealth);
        _healthBarPropertyBlock.SetFloat("_MaxValue", _totalHealth);
        _healthBarPropertyBlock.SetFloat("_DamagedValue", _damagedHealthRatio );
        _healthBarPropertyBlock.SetFloat("_Alpha", alpha);

        _healthBarRenderer.SetPropertyBlock(_healthBarPropertyBlock);

        if (_damagedHealthRatio > 0) {
            _damagedHealthRatio = Mathf.Max(0, _damagedHealthRatio - Time.deltaTime * 0.4f);
        }
    }
}
