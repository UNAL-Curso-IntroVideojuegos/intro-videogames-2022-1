using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject BulletInicio;
    public GameObject BulletPrefab;
    public float BulletSpeed;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject BulletTemporal = Instantiate(BulletPrefab, BulletInicio.transform.position, BulletInicio.transform.rotation) as GameObject;
            Rigidbody rb = BulletTemporal.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * BulletSpeed);
            Destroy(BulletTemporal, 5f);
        }
    }
}