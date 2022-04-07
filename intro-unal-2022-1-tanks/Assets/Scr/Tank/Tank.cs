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
        Debug.Log("Hello World");
        
    }

    // Update is called once per frame
    void Update()
    {

        // -1 Abajo, 0 No oprime, 1 Arriba
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;


        //Rotation
        // _turret.LookAt(worldPos, Vector3.up); // Para 3D

        Vector3 aimVector = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

        // _turret.up = aimVector.normalized;


        // Movement
        // if (vertical != 0){
        //     Vector3 pos = transform.position;
        //     pos += vertical * speed * Time.deltaTime * transform.up;
        //     transform.position = pos;
        // }
        // if (horizontal != 0){
        //     Vector3 pos = transform.position;
        //     pos += horizontal * speed * Time.deltaTime * transform.right;
        //     transform.position = pos;
        // }
        Vector3 dir = new Vector3(horizontal, vertical);
        dir.Normalize();

        Vector3 pos = transform.position;
        // Vector3 movement = speed * Time.deltaTime * dir;
        // movement = transform.rotation * movement;
        // pos += movement;
        pos += dir.y * speed * Time.deltaTime * transform.up;
        pos += dir.x * speed * Time.deltaTime * transform.right;
        transform.position = pos;
        
    }
}
