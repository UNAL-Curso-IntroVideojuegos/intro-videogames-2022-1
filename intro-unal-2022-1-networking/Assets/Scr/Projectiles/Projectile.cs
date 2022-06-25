using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _timeToDisable = 3;
    [SerializeField]
    private LayerMask _collisionMask;

    private SpaceShipController _spaceShip;
    
    public void SetPlayer(SpaceShipController spaceShip) => _spaceShip = spaceShip;
    
    void Update()
    {
        if(!IsServer)
            return;
        
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, translation.magnitude, _collisionMask);
        if (hit.transform != null && hit.transform != _spaceShip.transform)
        {
             if (hit.transform.TryGetComponent(out SpaceShipController target)) 
             {
                 Debug.LogError("Damage to other player");
                 //target.TakeDamage(10);
             }
             
             DestroyProjectile();
        }
    }
    
    private void DestroyProjectile()
    {
        if(!IsServer)
            return;
        
        Destroy(gameObject);
    }
    
}
