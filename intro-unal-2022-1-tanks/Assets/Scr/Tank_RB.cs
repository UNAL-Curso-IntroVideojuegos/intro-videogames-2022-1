using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_RB : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    private void Start()
    {
    }

    private void Update()
    {
        //transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //_rb.velocity = Vector3.zero;
    }
}
