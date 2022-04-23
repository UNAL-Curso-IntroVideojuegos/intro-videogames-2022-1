using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private LayerMask _collisionMask;
    [SerializeField]
    private float DashTime = 1;
    
    private Camera _cam;
    private Rigidbody _rb;
    private Vector3 _velocity;
    private float Timer = 0;
    private float InitialSpeed = 5;


    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        InitialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
       
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           if (speed < InitialSpeed * 10) 
           {
               speed = speed * 10;
               Timer = 0;
           }
        }



        Vector2 mousePos= Input.mousePosition;
        Ray ray =_cam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hitInfo;
        bool hitSomething = Physics.Raycast(ray, out hitInfo , 500, _collisionMask);
        if (hitSomething)
        {
            
            Vector3 dir = (hitInfo.point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir); 
        }

        Vector3 _dir = new Vector3(horizontal, 0 , vertical);
        _dir.Normalize();
        _velocity = (speed * _dir);
        
        Timer += Time.deltaTime;

        if (Timer >= DashTime)
        {
           speed = InitialSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }
}
