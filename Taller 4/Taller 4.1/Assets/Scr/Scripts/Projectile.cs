using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _Velocidad = 7;
    private float Time = 2f;
    private float Distance;
    private Rigidbody _rb;
    private Vector3 Inicio;
    private LayerMask _Colision;
    
    private void Start()
    {
    Init();
    }

    private void Init()
    {
    _rb = GetComponent<Rigidbody>();
    Inicio = _rb.position;
    Distance = 0;
    DestroyObjectDelayed();
    }

    private void FixedUpdate()
    {
    Vector3 dir = transform.forward;
    Vector3 movement = dir * _Velocidad * Time.fixedDeltaTime;
    Vector3 pos = _rb.position + movement;
    CheckCollision(movement);
    _rb.MovePosition(pos);
    }

    private void CheckCollision(Vector3 movement)
    {
    RaycastHit hit;
    bool isHitting = Physics.Raycast(_rb.position, transform.forward, out hit, movement.magnitude, _Colision);
    Distance = (Inicio - _rb.position).magnitude;
    if (hit.collider != null)
   {
    Debug.Log("Colision " + hit.collider.name);
    DestroyProjectile();
  }
    }

    void DestroyObjectDelayed()
    {
    Destroy(gameObject, Time);
    }

    private void DestroyProjectile()
    {
    Destroy(gameObject);      
    }
}