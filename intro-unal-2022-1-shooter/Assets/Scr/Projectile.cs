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
    private float _lifeTime = 3;
    
    private float _waitTime;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _waitTime = _lifeTime;
    }

    private void Update(){
        if(_waitTime<= 0){
            DestroyProjectile();
        }

        _waitTime -= Time.deltaTime;

        float movementDistance = _speed * Time.deltaTime; //X = V * T
        Vector3 translation = Vector3.forward * movementDistance;
        OnCollision(translation);
        transform.Translate(translation);
    }

    private void OnCollision(Vector3 translation)
    {
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, translation.magnitude, _collisionMask))
        {
            //Debug.Log("Hit with " + hit.collider.name);
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
