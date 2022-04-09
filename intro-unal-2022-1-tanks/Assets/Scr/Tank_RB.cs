using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_RB : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;
    

    private void Start()
    {
        //_rb.velocity = new Vector2(0, 1);
    }

    private void Update()
    {
        //transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        Vector3 aimVector = mouseWorldPos - transform.position;
        _turret.up = aimVector.normalized;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical);
        dir.Normalize();

        transform.position += speed * Time.deltaTime * (transform.rotation * dir);;
    }
}
