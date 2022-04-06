using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float min;
    [SerializeField]
    private float max ;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform _turret;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,min,0);
    }

    private void Update()
    {
       
       transform.position= new Vector3(transform.position.x,Mathf.PingPong(Time.time,max-min)+min,0);

        //mov torreta 

        Vector3 aimVector = player.position - _turret.position;
        aimVector.Normalize();
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg -180;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
    }
}
