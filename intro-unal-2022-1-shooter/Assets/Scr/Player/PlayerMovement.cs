using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]    private float speed = 2;
    [SerializeField]    private float _timer;
    [SerializeField]    private float timeDash;
    [SerializeField]    private bool doDash = false;
    
    [SerializeField]    private float speedDash;
    
                        private LayerMask _collisionMask;
    
    [Header("Mouse and rotation")]
    
    [SerializeField]    private bool _usePlaneForRotation = false;
    
    private Camera _cam;
    private Plane _woldPlane;
    
    private Rigidbody _rb;
    private Vector3 _velocity;

    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();

        _woldPlane = new Plane(Vector3.up, 0);

        _timer = 0;

        timeDash = 0.1f;

        speedDash = 100;
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (_usePlaneForRotation)
        {
            LookAtMousePointWithPlane(ray);
        }
        else
        {
            LookAtMousePointWithRaycast(ray);
        }

        
        Vector3 _dir  = new Vector3(horizontal, 0, vertical);
        _dir.Normalize();

        //Leemos si se presiona la tecla shift y cambiamos la variable doDash
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            doDash = true;
            }

        //Si se activa el Dash ejecutamos esta parte
        if (doDash)
        {   
            //Definimos la nueva velocidad
            _velocity = speedDash * _dir;
            //Contamos cuanto tiempo va pasando
            _timer += Time.deltaTime;
            //si supera el umbral, detenemos el dash y reiniciamos el contador
            if(_timer > timeDash){
                _timer = 0.0f;
                doDash = false;
            }
        } else {
            _velocity = speed * _dir;
        }
    }

    private void FixedUpdate()
    {
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        _rb.velocity = _velocity;
    }
    
    //Calculate Mouse world position using Physics world and Physics.Raycast
    private void LookAtMousePointWithRaycast(Ray ray)
    {
        RaycastHit hitInfo;
        // Does the ray intersect any objects excluding the player layer
        bool hitSomething = Physics.Raycast(ray, out hitInfo, 500,_collisionMask); 
        if (hitSomething)
        {
            //transform.position = hitInfo.point;
            Vector3 point = hitInfo.point;
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            //transform.forward = dir;
            //transform.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
            transform.LookAt(point);
        }
    }
    
    //Calculate Mouse world position using internal Plane object and Plane.Raycast
    private void LookAtMousePointWithPlane(Ray ray)
    {
        float distanceToPlane;
        bool hitSomething = _woldPlane.Raycast(ray, out distanceToPlane);
        if (hitSomething)
        {
            Vector3 point = ray.GetPoint(distanceToPlane);
            point.y = transform.position.y;
            Vector3 dir = (point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir); //Mire a la direccion
        }
    }
    
}
