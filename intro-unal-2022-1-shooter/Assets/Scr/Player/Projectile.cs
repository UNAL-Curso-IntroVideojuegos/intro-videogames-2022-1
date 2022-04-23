using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7;

    void Start()
    {
        
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos += _speed * Time.deltaTime * transform.forward;
    
        transform.position = pos;
    }
}
