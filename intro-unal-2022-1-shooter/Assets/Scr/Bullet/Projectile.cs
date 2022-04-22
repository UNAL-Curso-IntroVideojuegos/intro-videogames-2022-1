using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float _bulletLifeTime = 5;
    [SerializeField]

    private float _speed = 5;
    private Rigidbody _rb;
    private Vector3 _velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _bulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = transform.forward * _speed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }

}
