using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
    }

    // Destruccion del proyectil ante colision
    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
