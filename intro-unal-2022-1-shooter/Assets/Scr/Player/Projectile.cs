using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private Rigidbody _rb;
    [SerializeField]
    private float timedestroy=5;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
    }
    void Update()
    {
        Destroy(gameObject,timedestroy);
    }
}
