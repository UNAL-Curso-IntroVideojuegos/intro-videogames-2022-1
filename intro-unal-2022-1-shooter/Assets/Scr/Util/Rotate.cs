using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed = 90;
    
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * Speed);
    }
}
