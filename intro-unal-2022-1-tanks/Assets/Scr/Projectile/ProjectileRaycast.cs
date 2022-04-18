using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRaycast : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7;
    [SerializeField]
    private float maxDistance = 10;
    [SerializeField]
    private LayerMask _collisionMask;
    
    private Rigidbody2D _rb;
    private Vector2 initialPosition;
    private float currentDistance;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        initialPosition = _rb.position;
        currentDistance = 0;
    }

    private void FixedUpdate()
    {
        Vector2 dir = transform.up;
        Vector2 movement = dir * _speed * Time.fixedDeltaTime;
        
        Vector2 pos = _rb.position + movement;

        
        CheckCollision(movement);
        
        _rb.MovePosition(pos);
    }

    private void CheckCollision(Vector2 movement)
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, transform.up, movement.magnitude, _collisionMask);

        currentDistance = (initialPosition - _rb.position).magnitude;
        
        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("Hit with " + hit.collider.name);
            Enemy enemy = hit.collider.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
            
            DestroyProjectile();
        }
        if (currentDistance >= maxDistance){
            DestroyProjectile();
        }
    }


    private void DestroyProjectile()
    {
        // gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
