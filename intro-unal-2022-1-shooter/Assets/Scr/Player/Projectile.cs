using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed = 10;

    private Rigidbody _rb;
    private Vector3 _velocity;
   
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 _dir  = transform.forward;
        _dir.Normalize();
        _velocity = speed * _dir;
    }
    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        
        _rb.velocity =  _velocity;

        
    }
}
