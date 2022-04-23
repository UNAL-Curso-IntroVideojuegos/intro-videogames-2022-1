using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 15;
    private float bulletTime = 0;
    private float bulletTotalTime = 3;

    private Rigidbody _rb;
    private Vector3 vel;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        vel = bulletSpeed * transform.forward;
        if (bulletTime >= bulletTotalTime) { Destroy(gameObject); }
        bulletTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.velocity = vel;
    }
}
