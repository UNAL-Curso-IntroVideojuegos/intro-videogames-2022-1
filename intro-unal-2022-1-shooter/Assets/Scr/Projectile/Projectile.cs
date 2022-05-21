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

    private void CheckCollision(Vector3 translation)
    {
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, translation.magnitude, _collisionMask))
        {
            //Debug.Log("Hit with " + hit.collider.name);
            // if (hit.transform.GetComponent<LivingEntity>() != null)
            // {
            //     //Haga daño
            //     LivingEntity entity = hit.transform.GetComponent<LivingEntity>();
            //     entity.TakeDamage(10);
            // }
            // Esto es lo mismo que usar lo anterior:
            if (hit.transform.TryGetComponent(out LivingEntity entity))
            {
                entity.TakeDamage(10);
            }
            
            DestroyProjectile();
        }
    }


    private void DestroyProjectile()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}