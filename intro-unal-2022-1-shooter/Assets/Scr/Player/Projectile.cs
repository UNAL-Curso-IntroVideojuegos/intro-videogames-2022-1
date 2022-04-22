using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _timeDestroy = 1;
    private Rigidbody _rb;
    private float _speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
          
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, _timeDestroy);
        
    }
}
