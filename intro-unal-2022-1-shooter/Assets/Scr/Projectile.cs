using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float pspeed = 20;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward*pspeed;
    }
    void Update(){
        Destroy(gameObject, 4f);
    }
    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
