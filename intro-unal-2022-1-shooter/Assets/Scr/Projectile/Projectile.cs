using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inicializando variables
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private Rigidbody _rb;
    [SerializeField]
    private float timedestroy=5;
    
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