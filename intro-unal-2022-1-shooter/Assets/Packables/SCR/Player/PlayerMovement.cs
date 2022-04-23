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
    
    private Camera _cam;
    private Plane _woldPlane;

    private Rigidbody _rb;
    private Vector3 _velocity;

    private bool dashing = false;
    private bool jumping = false;
    [SerializeField]
    private float dashSpeed = 25;
    [SerializeField]
    private float dashFriction = 40;
    private float currentDashSpeed;
    private float currentJumpSpeed;
    private float jumpSpeed = 4;
    private Vector3 dashDirection;
    private Vector3 jumpVelocity;
    private Vector3 dashVelocity;

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool dash = Input.GetButtonDown("Fire3");
        bool jump = Input.GetButtonDown("Jump");

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hitSomething = Physics.Raycast(ray, out hitInfo, 500, _collisionMask);
        if (hitSomething)
        {
            Vector3 point = hitInfo.point;   
            Vector3 dir = (point - transform.position).normalized;
            transform.LookAt(point);
        }
        if (jump && !jumping)
        {
            jumping = true;
            currentJumpSpeed = jumpSpeed;
        }
        if (jumping)
        {
            jumpVelocity = Jump();
        }
        else
        {
            jumpVelocity = new Vector3(0, 0, 0);
        }
        if (dash && !dashing)
        {
            dashing = true;
            currentDashSpeed = dashSpeed;
        }
        if (dashing)
        {
            _velocity = Dash(dashDirection);
        }
        else
        {
         Vector3 _dir = new Vector3(horizontal, 0, vertical);
            _dir.Normalize();
            _velocity = speed * _dir;
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = _velocity + jumpVelocity;
    }
    private void LookAtMousePointWithRaycast(Ray ray)
    {
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
        Vector3 directionVelocity = transform.forward.normalized * finalSpeed;
        return directionVelocity;
    }
    private Vector3 Jump()
    {
        float finalSpeed = (currentJumpSpeed + Physics.gravity.y * Time.deltaTime);
        finalSpeed = Mathf.Clamp(finalSpeed, -jumpSpeed, jumpSpeed);
        if (finalSpeed == -jumpSpeed)
        {
            jumping = false;
        }
        currentJumpSpeed = finalSpeed;
        Vector3 directionVelocity = transform.up.normalized * finalSpeed;
        return directionVelocity;
    }

}