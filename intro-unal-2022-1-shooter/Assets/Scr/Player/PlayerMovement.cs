using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private LayerMask _collisionMask;
    
    [SerializeField]
    private bool _usePlaneForRotation = false;
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private Vector3 _velocity;

    float saltarAmount = 50;
    float gravityScale = 10;
    bool saltar;
    
    float distDash = 7;
    float stop = 0.1f;
    float timeDash = 1.0f;
    Vector3 direction;
    

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _woldPlane = new Plane(Vector3.up, 0);
        saltar = false;
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 _dir  = new Vector3(horizontal, 0, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            timeDash = 0;                
        }
        if (timeDash < 1.0f) {
            direction = transform.forward * distDash;
            timeDash = timeDash + stop;
        } else {
            direction = Vector3.zero;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            saltar = true;
        }
        
        
    }

    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        _rb.velocity = _velocity;
    }
    
    //Calculate Mouse world position using Physics world and Physics.Raycast
    private void LookAtMousePointWithRaycast(Ray ray)
    {
        RaycastHit hitInfo;
        // Does the ray intersect any objects excluding the player layer
        bool hitSomething = Physics.Raycast(ray, out hitInfo, 500,_collisionMask); 
        if (hitSomething)
        {
            //transform.position = hitInfo.point;
            Vector3 point = hitInfo.point;
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            //transform.forward = dir;
            //transform.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
            transform.LookAt(point);
        }
    }
    
    //Calculate Mouse world position using internal Plane object and Plane.Raycast
    private void LookAtMousePointWithPlane(Ray ray)
    {
        float distanceToPlane;
        bool hitSomething = _woldPlane.Raycast(ray, out distanceToPlane);
        if (hitSomething)
        {
            Vector3 point = ray.GetPoint(distanceToPlane);
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
        }
    }
    
    
    private void mousepoint(Ray ray)
    {
        RaycastHit hitInfo;
        bool hitSomething = Physics.Raycast(ray, out hitInfo, 500,_collisionMask); 
        
        if (hitSomething)
        {
            Vector3 point = hitInfo.point;
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.LookAt(point);
        }
    }

}
