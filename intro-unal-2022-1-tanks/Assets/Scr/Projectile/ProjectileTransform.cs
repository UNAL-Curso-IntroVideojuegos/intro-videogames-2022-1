using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTransform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _lifetime = 2f; //In sec.
    [SerializeField]
    private float _initCheckRadius = 0.25f;
    [Header("Testing...")]
    public LayerMask maskCollision;

    private float _timeToDisable = 3;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _timeToDisable = _lifetime;
    }
    
    private void Update()
    {
        if (_timeToDisable <= 0)
        {
            DestroyProjectile();
            return;
        }
        
        _timeToDisable -= Time.deltaTime;
        
        float movementDistance = _speed * Time.deltaTime; //X = V * T
        Vector3 translation = Vector3.up * movementDistance;
        CheckCollision(translation);
        transform.Translate(translation);
    }
    
    private void CheckCollision(Vector3 translation)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, translation.magnitude, maskCollision);
        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent<Enemy>(out Enemy target))
            {
                target.TakeDamage();
            }
            
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        //TODO: Pool
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _initCheckRadius);
    }
}
