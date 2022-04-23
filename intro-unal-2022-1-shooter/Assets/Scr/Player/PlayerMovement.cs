using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private LayerMask _collisionMask;
    
    [Header("Mouse and rotation")]
    [SerializeField]
    private bool _usePlaneForRotation = false;
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private Vector3 _velocity;

    float timeDash = 0;
    float timeRest = 0;

    bool dash1;

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();

        _woldPlane = new Plane(Vector3.up, 0);
    }

    void Update()
    {

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (_usePlaneForRotation)
        {
            LookAtMousePointWithPlane(ray);
        }
        else
        {
            LookAtMousePointWithRaycast(ray);
        }

    }

    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool dash = Input.GetKey("left shift");

        Vector3 _dir = new Vector3(horizontal, 0, vertical);
        _dir.Normalize();

        if (dash)
        {
            dash1 = true;
        }

        if (dash1 && timeDash < 0.1)
        {
            timeDash = timeDash + Time.deltaTime;
            _velocity = 10 * speed * _dir;
            timeRest = timeRest + Time.deltaTime;
        }

        else if (timeDash >= 0.1 && timeRest < 2)
        {
            timeRest = timeRest + Time.deltaTime;
            _velocity = speed * _dir;
            dash1 = false;
        }

        else
        {
            timeDash = 0;
            timeRest = 0;
            _velocity = speed * _dir;
        }

        print(dash);

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
    
}
