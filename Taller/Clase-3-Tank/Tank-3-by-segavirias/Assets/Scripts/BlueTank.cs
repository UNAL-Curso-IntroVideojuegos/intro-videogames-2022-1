using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTank : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    
    void Start()
    {

    }
    

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical);

        dir.Normalize();

        Vector3 pos = transform.position;
        pos +=  dir.y * speed * Time.deltaTime * transform.up; //X = V * T
        pos +=  dir.x * speed * Time.deltaTime * transform.right;
        
        transform.position = pos;
   
    }

}
