using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform _body;
    [SerializeField]
    private Animator _animator;

    private bool _isShooting = false;
    private bool _isReloading = false;
    private Vector3 _movementDirection;

    public void SetIsShooting(bool isShooting)
    {
        _isShooting = isShooting;
    }
    
    public void SetIsReloading(bool isReloading)
    {
        _isReloading = isReloading;
    }

    public void SetMovementDirection(Vector3 movementDirection)
    {
        _movementDirection = movementDirection;
    }
    
    void Update()
    {
        // Pasar la direccion global (Input) -> La direccion local (en base a donde estoy mirando)
        Vector3 localRotation = Quaternion.Inverse(_body.rotation) * _movementDirection;
        
        _animator.SetBool("IsShooting", _isShooting);
        _animator.SetBool("IsReloading", _isReloading);
        _animator.SetFloat("MovementX", localRotation.x);
        _animator.SetFloat("MovementY", localRotation.z);
    }
}
