using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    
    void Start()
    {
        // Debug.Log("Hello World!");
    }
    private void OnEnable()
    {
        //Debug.Log("On!!!");
    }
    private void OnDisable()
    {
        //Debug.Log("Off!!!");
    }
    private void OnDestroy()
    {
        //Debug.LogError("Me mori :("));
    }

    void Update()
    {
        //Debug.Log("Updating...");

        //-1: Abajo. 
        // 0: No oprime
        // 1: Arriba
        //float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //if (Input.GetKey(KeyCode.A)) { }
        //if (Input.GetButton("Jump")) { }

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
}
