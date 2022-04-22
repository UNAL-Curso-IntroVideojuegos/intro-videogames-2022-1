using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private LayerMask _collisionMask;
    
    [Header("Mouse and rotation")]
    [SerializeField]
    private bool _usePlaneForRotation = false;
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private Vector3 _velocity;

    private bool dashing = false;
    private bool jumping = false;
    [SerializeField]
    private float dashSpeed = 20;
    [SerializeField]
    private float dashFriction = 20;
    private float currentDashSpeed;
    private float currentJumpSpeed;
    private float jumpSpeed = 5;
    private Vector3 dashDirection;
    private Vector3 jumpVelocity;
    private Vector3 mouseRelativeDirection;
    

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
        bool dash = Input.GetButtonDown("Fire3");
        bool jump = Input.GetButtonDown("Jump");
        
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        
        if (_usePlaneForRotation)
        {
            mouseRelativeDirection = LookAtMousePointWithPlane(ray);
        }
        else
        {
            mouseRelativeDirection = LookAtMousePointWithRaycast(ray);
        }

        if(jump && !jumping)
        {
            Debug.Log("Jump");
            jumping = true;
            currentJumpSpeed = jumpSpeed;
        }

        if(jumping){
            jumpVelocity = Jump();
        }
        else
        {
            jumpVelocity = new Vector3(0,0,0);
        }

        if (dash && !dashing)
        {
            Debug.Log("Dash");
            dashing = true;
            currentDashSpeed = dashSpeed;
            dashDirection = mouseRelativeDirection;
        } 

        if ( dashing )
        {
            _velocity = Dash(dashDirection);
        }
        else
        {
            Vector3 _dir  = new Vector3(horizontal, 0, vertical);
            _dir.Normalize();
            _velocity = speed * _dir;
        }
        

    }

    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        _rb.velocity = _velocity + jumpVelocity;
    }
    
    //Calculate Mouse world position using Physics world and Physics.Raycast
    private Vector3 LookAtMousePointWithRaycast(Ray ray)
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
            return dir;
        }
        return new Vector3(1,0,0);
    }
    
    //Calculate Mouse world position using internal Plane object and Plane.Raycast
    private Vector3 LookAtMousePointWithPlane(Ray ray)
    {
        float distanceToPlane;
        bool hitSomething = _woldPlane.Raycast(ray, out distanceToPlane);
        if (hitSomething)
        {
            Vector3 point = ray.GetPoint(distanceToPlane);
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
            return dir;
        }
        return new Vector3(1,0,0);
    }

    private Vector3 Dash(Vector3 direcion)
    {
        float finalSpeed = (currentDashSpeed - dashFriction * Time.deltaTime);
        finalSpeed = Mathf.Max(0, finalSpeed);
        if (finalSpeed == 0)
        {
            dashing = false;
        }
        currentDashSpeed = finalSpeed;
        Vector3 directionVelocity = direcion * finalSpeed;
        return directionVelocity;
    }

        private Vector3 Jump()
    {
        float finalSpeed = (currentJumpSpeed + Physics.gravity.y * Time.deltaTime);
        finalSpeed = Mathf.Clamp(finalSpeed, - jumpSpeed, jumpSpeed);
        if (finalSpeed == - jumpSpeed)
        {
            jumping = false;
        }
        currentJumpSpeed = finalSpeed;
        Vector3 directionVelocity = transform.up.normalized * finalSpeed;
        return directionVelocity;
    }
    
}
