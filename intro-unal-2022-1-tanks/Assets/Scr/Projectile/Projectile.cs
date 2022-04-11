using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 7;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
        // if (other.transform.TryGetComponent<Enemy>(out Enemy enemy))
        // { 
        //     enemy.TakeDamage();    
        // }

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
