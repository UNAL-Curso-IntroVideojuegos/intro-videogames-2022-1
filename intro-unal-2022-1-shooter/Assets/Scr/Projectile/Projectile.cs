using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Rigidbody cuerpo;
    private float time = 1f;

    private void Start() {
        Init();
    }
    private void Init(){
        cuerpo = GetComponent<Rigidbody>();
        cuerpo.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,time);
    }
}
