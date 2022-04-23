using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float DestroyTime = 5;

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 dir =  transform.forward;
        Vector3 movement = dir * speed * Time.fixedDeltaTime;
        Vector3 pos = _rb.position + movement;

        _rb.MovePosition(pos);

        Destroy(gameObject, DestroyTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
