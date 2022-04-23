using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float vidaproyectil = 5;
    [SerializeField]

    public float Velocidad = 3;
    private Rigidbody RB;
    private Vector3 _velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Destroy(gameObject, vidaproyectil);
    }

    // Update is called once per frame
    void Update()
    {
         _velocity = transform.forward * Velocidad;
    }
    
    private void FixedUpdate()
    {
        RB.velocity = _velocity;
    }
}
