using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 7;
    [SerializeField]
    private Rigidbody _rb;
    
    private float time_destroy = 1f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
        DestroyBullet();

    }
    private void DestroyBullet()
    {
        Destroy(gameObject, time_destroy);
    }

}
