using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform tank;
    [SerializeField]
    private Transform _turret;

    [SerializeField]
    private Transform _turret_2;

    [SerializeField]
    private float minim;
    [SerializeField]
    private float maxim;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float dir = 1;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        // movimiento del tanque en eje y
        Vector3 ubi = transform.position;
        if (ubi.y == maxim){
            dir = -1;
        }
        if (ubi.y == minim){
            dir = 1;
        }
        ubi.y += Time.deltaTime*speed*dir;
        ubi = new Vector3(ubi.x,Mathf.Clamp(ubi.y,minim,maxim));
        transform.position = ubi;


        //torretas mirando al tanque
        Vector3 tankPos = tank.position;

        Vector3 aimVector = tankPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

        //Vector3 rot = _turret_2.eulerAngles;
        _turret_2.eulerAngles = rot;


    }
}