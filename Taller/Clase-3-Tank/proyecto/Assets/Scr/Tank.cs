using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;

    void Start()
    {
        // Debug.Log("Hello World!");
    }

    void Update()
    {
       
    }
}
