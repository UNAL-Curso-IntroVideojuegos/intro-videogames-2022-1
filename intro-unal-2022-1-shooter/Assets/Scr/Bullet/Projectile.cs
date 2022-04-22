using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10.0f;
    private float bulletLifeTime = 0;
    private float maxBulletLifeTime = 3;
    private Vector3 velocity;
    private Rigidbody _rb; 
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        velocity = bulletSpeed * transform.forward;
        if (bulletLifeTime > maxBulletLifeTime)
        {
            DestroyProjectile();
        }
        bulletLifeTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.velocity = velocity;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
