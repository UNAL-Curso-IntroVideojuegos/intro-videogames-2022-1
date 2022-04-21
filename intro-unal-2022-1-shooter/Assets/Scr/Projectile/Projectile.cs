using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7;
    [SerializeField]
    private LayerMask _collisionMask;
    [SerializeField]
    private float _lifetime = 3;
    
    private float _timeToDisable = 3;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _timeToDisable = _lifetime;
        //Alternatives:
        // Destroy(gameObject, _timeToDisable);
        // Invoke("DestroyProjectile", _timeToDisable);
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
        Vector3 translation = Vector3.forward * movementDistance;
        CheckCollision(translation);
        transform.Translate(translation);
    }

    private void CheckCollision(Vector2 translation)
    {
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, translation.magnitude, _collisionMask))
        {
            Debug.Log("Hit with " + hit.collider.name);
            // if (hit.transform.TryGetComponent<IDamageable>(out IDamageable target))
            // {
            //     target.TakeHit(Random.Range(2,5), hit.point, hit.normal);
            // }
            
            DestroyProjectile();
        }
    }


    private void DestroyProjectile()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}