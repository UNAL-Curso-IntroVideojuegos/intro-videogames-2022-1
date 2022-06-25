using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private const float INTERPOLATION_TIME = 0.2f;
    
    [SerializeField]
    private float _movementAcceleration = 8;
    [SerializeField]
    private float _movementDesAcceleration = 8;
    [SerializeField]
    private float _movementMaxSpeed = 10;
    [SerializeField]
    private float _rotationSpeed = 10;

    private SpaceShipController _shipController;
    private Rigidbody2D _rb;
    
    public Vector2 Position => _rb == null ? Vector2.zero : _rb.position;
    
    //Only used if Local movement
    private Vector2 _input;
    private float _targetZRot;
    private float _movementSpeed;

    //Only used if Network Movement
    private Vector2 _targetPosition; 
    private Vector3 _targetRotation;
    private Vector2 _posVel;
    private float _rotVelZ;

    public void SetTargetMovementData(Vector2 position, Vector3 rotation)
    {
        _targetPosition = position;
        _targetRotation = rotation;
    }
    
    private void Start()
    {
        _shipController = GetComponent<SpaceShipController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_shipController.IsOwner)
        {
            _input = _shipController.ShipInput.InputAxis;
        }
    }

    private void FixedUpdate()
    {
        if (_shipController.IsOwner)
        {
            CalculateMovement();
            ApplyMovement();
        }
        else
        {
            ForceSmoothMovement();
        }
        
        _rb.velocity = Vector2.zero;
    }

    private void CalculateMovement()
    {
        if (_input != Vector2.zero)
        {
            //Movement
            _movementSpeed += _movementAcceleration * Time.fixedDeltaTime;

                //Rotation
            Vector2 inputNormalize = _input.normalized;
            _targetZRot = Mathf.Atan2(inputNormalize.y, inputNormalize.x) * Mathf.Rad2Deg - 90f;
        }
        else
        {
            _movementSpeed -= _movementDesAcceleration * Time.fixedDeltaTime;
            _targetZRot = transform.eulerAngles.z;
        }
        
        _movementSpeed = Mathf.Clamp(_movementSpeed, 0, _movementMaxSpeed);
    }

    private void ApplyMovement()
    {
        transform.position += _movementSpeed * Time.fixedDeltaTime * transform.up;
        transform.rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _targetZRot), _rotationSpeed * Time.fixedDeltaTime);
    }

    private void ForceSmoothMovement()
    {
        // Here you'll find the cheapest, dirtiest interpolation you'll ever come across. Please do better in your game
        _rb.MovePosition(Vector2.SmoothDamp(_rb.position, _targetPosition, ref _posVel, INTERPOLATION_TIME));
        transform.rotation = Quaternion.Euler(
            0, 0 ,Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, _targetRotation.z, ref _rotVelZ, INTERPOLATION_TIME));
    }
}
