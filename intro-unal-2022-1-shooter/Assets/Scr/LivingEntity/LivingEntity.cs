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
    
    public virtual void Start()
    {
        _health = _totalHealth;
        
        Debug.Log("LE Start");
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
}