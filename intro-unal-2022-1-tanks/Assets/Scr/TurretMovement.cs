using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    Tank tank;
    public GameObject Tank;


    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret;

    [SerializeField]
    private double maxY = 5.0;

    [SerializeField]
    private double minY = 0.0;


    private int i = 1;
    private int j;
    private double x1;
    private double x2;
    private double y1;
    private double y2;
    private double angle;

    void Awake()
    {
        tank = Tank.GetComponent<Tank>();
    }

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

        Vector3 mov = transform.position;

        if (mov.y >= maxY)
        {
            i = -1;
            j = -1;
        }

        else if (mov.y <= minY)
        {
            i = 1;
            j = 1;
        }

        else
        {
            j = i;
        }

        mov.y += j * speed * Time.deltaTime;

        transform.position = mov;

        x1 = mov.x;
        y1 = mov.y;

        Vector3 mov2 = tank.transform.position;

        x2 = mov2.x;
        y2 = mov2.y;

        angle = Math.Atan((y2-y1)/(x2-x1))*(180/Math.PI);

        if((x2-x1<0 && y2-y1>0) || (x2 - x1 < 0 && y2 - y1 < 0))
        {
            angle = angle + 180;
        }

        Vector3 rot = _turret.eulerAngles;
        rot.z = Convert.ToSingle(angle);
        _turret.eulerAngles = rot;
    }
}