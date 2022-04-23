using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private LayerMask _collisionMask;
    
    [Header("Mouse and rotation")]
    [SerializeField]
    private bool _usePlaneForRotation = false;
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private Vector3 _velocity;

    //Inicializar variables Dash
    [SerializeField] private float dashSpeed = 2000;
    [SerializeField] private float dashTime = 0.2f;
    private float totalTime = 0;
    private bool dash = false;
    private Vector3 dashDirection;

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();

        _woldPlane = new Plane(Vector3.up, 0);
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (_usePlaneForRotation)
        {
            LookAtMousePointWithPlane(ray);
        }
        else
        {
            LookAtMousePointWithRaycast(ray);
        }

        
        Vector3 _dir  = new Vector3(horizontal, 0, vertical);
        _dir.Normalize();
        dashDirection = _dir;
        _velocity = speed * _dir;

        //Dash on/off
        if (dash == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) { dash = true; }
        }
        else
        {
            if (totalTime >= dashTime) { dash = false; totalTime = 0; }
            else { totalTime += Time.deltaTime; }
        }
    }

    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        _rb.velocity = _velocity;

        if (dash == true) { _rb.velocity = dashSpeed * dashDirection * Time.fixedDeltaTime; }
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
    
}
