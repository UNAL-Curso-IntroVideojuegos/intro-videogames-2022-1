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
        Debug.Log("Hello World!");
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
        //Debug.LogError("Me mori :(");
    }

    void Update()
    {
        //Debug.Log("Updating...");
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
    }
}
