using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7;
    [SerializeField]
    private float lifeTime = 1.5f;
    [SerializeField]
    private LayerMask _collisionMask;
    private Rigidbody _rb;
    private Vector3 initialPosition;
    private float currentDistance;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        initialPosition = _rb.position;
        currentDistance = 0;
        DestroyObjectDelayed();
    }

    private void FixedUpdate()
    {
        Vector3 dir = transform.forward;
        Vector3 movement = dir * _speed * Time.fixedDeltaTime;
        
        Vector3 pos = _rb.position + movement;
        
        CheckCollision(movement);
        
        _rb.MovePosition(pos);
    }

    private void CheckCollision(Vector3 movement)
    {
        RaycastHit hit;
        bool isHitting = Physics.Raycast(_rb.position, transform.forward, out hit, movement.magnitude, _collisionMask);

        currentDistance = (initialPosition - _rb.position).magnitude;
        
        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("Hit with " + hit.collider.name);
            DestroyProjectile();
        }
    }

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, lifeTime);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
