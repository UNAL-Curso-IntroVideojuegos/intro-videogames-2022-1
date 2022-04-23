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

    [SerializeField]
    private float _dashSpeed;
    [SerializeField]
    private float _dashTime;
    [SerializeField]
    private float _cooldownDash;
    private bool _dashing;
    private float _dashTimeCounter;
    private float _cooldown;
    private Vector3 _goDash;

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();

        _woldPlane = new Plane(Vector3.up, 0);

        _dashing = false;
        _dashTimeCounter = _dashTime;
        _cooldown = 0;
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
        _velocity = speed * _dir;

        if (!_dashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _cooldown < 0)
            {
                _dashing = true;
                _goDash = transform.forward;
                _cooldown = _cooldownDash + _dashTime;
            }
        }
        else
        {
            if(_dashTimeCounter <= 0f)
            {
                _dashing = false;
                _dashTimeCounter = _dashTime;
            }
            else
            {
                _dashTimeCounter -= Time.deltaTime;
            }
        }

        _cooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;

        if (_dashing)
        {
            _rb.velocity = _goDash * _dashSpeed * Time.fixedDeltaTime;
        }
    }

    private void LookAtMousePointWithRaycast(Ray ray)
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