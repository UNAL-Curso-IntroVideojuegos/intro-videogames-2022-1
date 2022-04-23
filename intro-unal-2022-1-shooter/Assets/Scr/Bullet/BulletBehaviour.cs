using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;
    
    [SerializeField] private float speed = 7f;
    
    private float _lifeRemaining = 0f;
    
    private Rigidbody _rb;

    private void Start()
    {
        Init();
        _lifeRemaining = 0f;
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * speed;
    }

    public void Update()
    {
        _lifeRemaining = Math.Min(lifeTime, _lifeRemaining + Time.deltaTime);
        if (Math.Abs(_lifeRemaining - lifeTime) < 0.1)
        {
            Destroy(gameObject);
        }
    }
}
