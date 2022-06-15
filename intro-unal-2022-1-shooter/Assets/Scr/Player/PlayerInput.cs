using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] 
    private bool _useMobileJoystick = false;
    [SerializeField] 
    private UIJoystick _leftJoystick;
    [SerializeField] 
    private UIJoystick _rightJoystick;
    
    [Header("Mouse and rotation")]
    [SerializeField]
    private bool _usePlaneForRotation = false;
    [SerializeField]
    private LayerMask _collisionMask;
    
    private Camera _cam;
    private Plane _woldPlane;
    private Vector3 _lastLookDirection;
    
    public Vector2 Movement => _useMobileJoystick ? _leftJoystick.Delta : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public bool Dash => Input.GetKeyDown(KeyCode.LeftShift);
    public bool Fire => _useMobileJoystick ? _rightJoystick.IsBeingHeld : Input.GetButton("Fire1");

    public void Start()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        _useMobileJoystick = true;
#endif
        _cam = Camera.main;
        _woldPlane = new Plane(Vector3.up, 0);
    }

    private void Update()
    {
        _leftJoystick.gameObject.SetActive(_useMobileJoystick);
        _rightJoystick.gameObject.SetActive(_useMobileJoystick);
    }

    public Vector3 GetLookDirection()
    {
        if (_useMobileJoystick)
        {
            if (_rightJoystick.IsBeingHeld)
            {
                _lastLookDirection = new Vector3(_rightJoystick.Delta.x, 0, _rightJoystick.Delta.y);
            }

            return _lastLookDirection;
        }
        
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Vector3 lookDirection = Vector3.zero;
        if (_usePlaneForRotation)
        {
            lookDirection = LookAtMousePointWithPlane(ray);
        }
        else
        {
            lookDirection = LookAtMousePointWithRaycast(ray);
        }

        if (lookDirection == Vector3.zero)
        {
            return _lastLookDirection;
        }

        _lastLookDirection = lookDirection;
        return lookDirection;
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
            //_body.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
            return dir;
        }

        return Vector3.zero;
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
            //_body.LookAt(point);
            return dir;
        }
        
        return Vector3.zero;
    }
    
}
