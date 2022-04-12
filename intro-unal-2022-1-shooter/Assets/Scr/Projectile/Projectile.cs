using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        Destroy(gameObject, _destroyTime);
    }
}
