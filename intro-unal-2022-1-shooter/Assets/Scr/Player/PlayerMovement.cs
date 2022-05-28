using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LivingEntity
{
    [SerializeField]
    private Transform _body;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private LayerMask _collisionMask;
    
    [Header("Mouse and rotation")]
    [SerializeField]
    private bool _usePlaneForRotation = false;

    [Header("Dash")]
    [SerializeField]
    private bool _useDashByTime = true;
    [SerializeField]
    private float _dashDistance = 3;
    [SerializeField] 
    private GameObject _dshVFX;
    [Header("Dash by Time")] 
    [SerializeField]
    private float _dashDuration = 0.1f;
    [Header("Dash by Drag")] 
    [SerializeField]
    private float _dashDragforce = 8f;
    
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private PlayerAnimation _playerAnimation;
    private Vector3 _velocity;
    
    //Dash
    private Vector3 _dashVelocity;
    //Dash by Time
    private float _dashTimer = 0;

    private Vector3 _initPosition;


    private void Awake()
    {
        _initPosition = transform.position;
    }

    public override void Start()
    {
        base.Start();
        
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        _woldPlane = new Plane(Vector3.up, 0);

        base.OnTakeDamage = OnTakeDamageCallback;
        
        _dshVFX.SetActive(false);
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool wantToDash = Input.GetKeyDown(KeyCode.LeftShift);
        
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
        Vector3 movementVelocity = speed * _dir;

        //Dash!
        if (_useDashByTime)
        {
            SetDashVelocityByTimer(wantToDash, _dir);
        }
        else
        {
            SetDashVelocityByDrag(wantToDash, _dir);
        }

        _velocity = movementVelocity + _dashVelocity;
        _playerAnimation.SetMovementDirection(_dir);
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
            _body.LookAt(point);
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
            _body.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
        }
    }
    
    
    private void SetDashVelocityByTimer(bool wantToDash, Vector3 dashDirection)
    {
        if (wantToDash && _dashTimer <= 0)
        {
            float dashSpeed = _dashDistance / _dashDuration; //V = X/T
            _dashVelocity = dashSpeed * dashDirection;
            _dashTimer = _dashDuration;
        }

        if (_dashTimer > 0)
        {
            _dashTimer -= Time.deltaTime;
        }
        else
        {
            _dashTimer = 0;
            _dashVelocity = Vector3.zero;
        }
        
        _dshVFX.SetActive(_dashTimer > 0);
    }
    
    private void SetDashVelocityByDrag(bool wantToDash, Vector3 dashDirection)
    {
        //We don't apply the dash if the speed is higher that the movement speed
        //  This mean, we don't dash if we are already dashing
        float dashMagnitude = _dashVelocity.magnitude;
        if (wantToDash && dashMagnitude <= speed)
        {
            float dashSpeed = _dashDistance / _dashDuration; //V = X/T
            //float dashSpeed = _dashDistance * _dashDragforce; //Kinda a hack to compensate the drag (multiplier)
            _dashVelocity = dashSpeed * dashDirection;
        }
        float multiplier = 1 - _dashDragforce * Time.deltaTime; 
        _dashVelocity *= multiplier ;
        
        _dshVFX.SetActive(dashMagnitude > speed);
    }


    private void OnTakeDamageCallback(int damage)
    {
        //int hearths = Mathf.RoundToInt((_health / _totalHealth) * _totalHealth);
        GameEvents.OnPlayerHealthChangeEvent?.Invoke((int) _health);
    }
    
    
    protected override void OnDeath()
    {
        base.OnDeath();
        GameManager.Instance.GameOver();
    }
    
    public override void Reset()
    {
        base.Reset();
        transform.position = _initPosition;
        gameObject.SetActive(true);
    }
}
