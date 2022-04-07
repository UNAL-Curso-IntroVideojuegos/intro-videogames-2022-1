using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_RB : MonoBehaviour
{
    // [SerializeField]
    // private Rigidbody2D _rb;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;

    private void Start()
    {
        // _rb.velocity = new Vector2(0, 1);
    }

    private void Update()
    {
        //Debug.Log("Updating...");
        //if (Input.GetKey(KeyCode.A)) { }
        //if (Input.GetButton("Jump")) { }

        //-1: Abajo. 
        // 0: No oprime
        // 1: Arriba
        //float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        
        //Rotation:
        //_turret.LookAt(worldPos); //Funciona en 3D, pero no en 2D
        Vector3 aimVector = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        //_turret.up = aimVector.normalized;
        

        //Movement:
        Vector3 dir = new Vector3(horizontal, vertical);
        //dir = dir.normalized;
        dir.Normalize();

        Vector3 pos = transform.position;
        pos +=  dir.y * speed * Time.deltaTime * transform.up; //X = V * T
        pos +=  dir.x * speed * Time.deltaTime * transform.right;
        
        //Vector3 movement = speed * Time.deltaTime * dir;
        //movement = transform.rotation * movement;
        //pos += movement;
        
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        //_rb.velocity = Vector3.zero;
    }
}
