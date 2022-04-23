using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]    private float _speed = 15;
    private Rigidbody _rb;
    [SerializeField]    private float timedestroy   =   10;

    //Método movimiento proyectiles
    private void Start()
    {
        Init();
    }

    //Creación método Init
    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
    }

    //Destrucción proyectiles
    void Update()
    {
        Destroy(gameObject,timedestroy);
    }
}
