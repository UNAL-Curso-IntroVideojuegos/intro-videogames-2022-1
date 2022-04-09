using System;
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

    void Start()
    {
        // Debug.Log("Hello World!");
    }
    private void OnEnable()
    {
        //Debug.Log("On!!!");
    }
    private void OnDisable()
    {
        //Debug.Log("Off!!!");
    }
    private void OnDestroy()
    {
        //Debug.LogError("Me mori :(");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;

        //Rotation:
        //_turret.LookAt(worldPos); //Funciona en 3D, pero no en 2D
        Vector3 aimVector = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

        //Movement:
        Vector3 dir = new Vector3(horizontal, vertical);
        dir.Normalize();

        Vector3 pos = transform.position;
        pos += speed * Time.deltaTime * (transform.rotation * dir);
        transform.position = pos;
    }
}
