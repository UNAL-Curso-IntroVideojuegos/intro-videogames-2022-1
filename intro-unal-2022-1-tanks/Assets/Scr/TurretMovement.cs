using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Transform _playerTank;
    [SerializeField]
    private Transform _turret1, _turret2;
    [SerializeField]
    private Transform _startPoint, _endPoint;
    [SerializeField] private float _speed = 1.5F;
    private Vector3 _dir;

    // Start is called before the first frame update
    void Start()
    {
        _dir = _startPoint.position - _endPoint.position;
        _dir.Normalize();
        
        transform.position = (_startPoint.position + _endPoint.position)/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 aimVector = _playerTank.position - transform.position;
        aimVector.Normalize();

        _turret1.up = aimVector;
        _turret2.up = aimVector;
        
        transform.position += _speed * Time.deltaTime * _dir;
    }

    void Test()
    {
        Tank t = new Tank();
        //t.speed = 3;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _dir *= -1;
    }
}
