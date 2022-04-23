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
    private float speeddash = 20;
    private float  timemaxdash= 1;
    private float timedash=0;
    private bool dash = false;
    private Vector3 _velocitydash;
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
        Vector3 _dir = new Vector3(horizontal, 0, vertical);
        _dir.Normalize();
        _velocity = speed*_dir;
        _velocitydash = speeddash* _dir;

        if (dash == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dash = true;
            }

        }
        else
        {
            if (timedash*4 >= timemaxdash) // Dash de 1/4 s
            {
                dash = false;
                timedash = 0;
            }
            else
            {
                timedash += Time.deltaTime;
            }

        }
    }
    private void FixedUpdate()
    {
        if (dash == true)
        {
            _rb.velocity = _velocitydash;
        }
        else
        {
            _rb.velocity = _velocity;
        }
    }
    private void LookAtMousePointWithRaycast(Ray ray)
    {
        RaycastHit hitInfo;
        bool hitSomething = Physics.Raycast(ray, out hitInfo, 500,_collisionMask); 
        if (hitSomething)
        {
            //transform.position = hitInfo.point;
            Vector3 point = hitInfo.point;
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.LookAt(point);
        }
    }
    private void LookAtMousePointWithPlane(Ray ray)
    {
        float distanceToPlane;
        bool hitSomething = _woldPlane.Raycast(ray, out distanceToPlane);
        if (hitSomething)
        {
            Vector3 point = ray.GetPoint(distanceToPlane);
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir); 
        }
    }
    
}
