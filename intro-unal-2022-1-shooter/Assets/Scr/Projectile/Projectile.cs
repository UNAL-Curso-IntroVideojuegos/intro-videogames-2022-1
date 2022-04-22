using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    
    public float velocidad = 10;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocidad;
    }

    
    void Update(){
        Destroy(gameObject, 3f); // El objeto se destruye despues de 3 segundos
    }
    
}

