using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    private Rigidbody _rb;
    private float timer;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _rb = GetComponent<Rigidbody>(); // Asigna el RigidBody 3D del objeto a la variable _rb
        _rb.velocity = transform.forward * _speed; // transform.up es (0, 0, 1), aquí se le esta asignado la velocidad en el eje Y del objeto
        timer = 0f;
    }

    private void Update(){
        timer += Time.deltaTime;
        if (timer > 3){
            Destroy(gameObject);
        }
    }
}
