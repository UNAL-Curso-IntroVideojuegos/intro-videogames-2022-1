using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rb;

    private void Start() {
        Init();
    }

    private void Init(){
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update(){
        // Destruccion del proyectil luego de 2 segundos
        Destroy(gameObject, 2f);
    }

    // Destruccion del proyectil ante colision
    void OnCollisionEnter(Collision other){
        Destroy(gameObject);
    }
}

