using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_RB : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;

    private Vector2 _velocity;
    
    private void Start()
    {
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        Vector2 _dir  = new Vector2(horizontal, vertical);
        _velocity.Normalize();
        _velocity = speed * _dir; // _velocity * speed;
        
        //Rotation:
        Vector3 aimVector = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
    }

    private void FixedUpdate()
    {
        //Dynimac
        _rb.velocity = _velocity;
        //_rb.AddForce();
        
        //Kinematic
        //Vector2 pos = _rb.position + _velocity * Time.fixedDeltaTime;
        //_rb.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision with " + other.transform.name);
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.LogWarning("Stay with " + other.transform.name);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.LogError("End collision with " + other.transform.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with " + other.transform.name);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.LogWarning("Trigger stay with " + other.transform.name);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.LogError("Trigger end with " + other.transform.name);
    }
}
