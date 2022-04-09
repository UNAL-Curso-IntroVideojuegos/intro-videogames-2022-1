using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input:
        float vertical = Input.GetAxisRaw("Vertical"); //-1: abajo, 0: no oprime, 1: arriba
        float horizontal = Input.GetAxisRaw("Horizontal"); //-1: abajo, 0: no oprime, 1: arriba

        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;

        //Rotation:
        //_turret.LookAt(worldPos); //Funciona en 3D, no en 2D
        Vector3 aimVector = mouseWorldPos - transform.position;
        //_turret.up = aimVector.normalized; //no recomendada
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

        //Movement:
        Vector3 dir = new Vector3(x: horizontal, y: vertical);
        // Normaliza el vector para que vaya a igual velocidad en la diagonal
        dir.Normalize(); //dir = dir.normalized; 

        Vector3 pos = transform.position; // Vector3 es struct
        Vector3 movement = speed * Time.deltaTime * dir;
        movement = transform.rotation * movement;
        pos += movement;
        // Tener en cuenta la suma de vectores, porque iría más rápido en diagonal
        //pos += dir.y * speed * Time.deltaTime * transform.up; //x=v*t
        //pos += dir.x * speed * Time.deltaTime * transform.right; //x=v*
        transform.position = pos;

    }
}
