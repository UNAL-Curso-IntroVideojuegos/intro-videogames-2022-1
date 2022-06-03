using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField]
    [Range(0,100)]
    protected float _totalHealth = 100;
    [SerializeField]
    protected float _health = 100;

    protected Action<int> OnTakeDamage;
    private bool _hasInitiliaze = false; 
    
    public bool IsDeath => _health <= 0;
    
    public virtual void Start()
    {
        _health = _totalHealth;
        Init();
    }

    public void Init()
    {
        if (_hasInitiliaze)
        {
            return;
        }

        _hasInitiliaze = true;
        OnInit();
    }
    
    protected virtual void OnInit()
    {
    }

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
        {
            return;
        }
        
        _health -= damage;
        OnTakeDamage?.Invoke(damage);
        
        if (_health <= 0)
        {
            _health = 0;
            OnDeath();
        }
    }
    
    protected virtual void OnDeath()
    {
    }

    public virtual void Reset()
    {
        _health = _totalHealth;
    }
}