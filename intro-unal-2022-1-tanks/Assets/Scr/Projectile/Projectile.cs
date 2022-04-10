using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _speed = 5;
    private Rigidbody2D _rb;

    private void Start(){
        Init();
    }

    private void Init() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other){
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
