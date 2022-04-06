using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform[] _turret;
    [SerializeField]
    private Transform[] ubicacion;
    [SerializeField]
    private float speed=1;
    private float time=0;
    private void Start()
    {
    }

    private void Update()
    {
        time += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(ubicacion[0].position, ubicacion[1].position, Mathf.PingPong(time,1));
       

        //mov torreta 
        for (int i = 0; i <= 1; i++)
        {
            Vector3 aimVector = player.position - _turret[i].position;
            aimVector.Normalize();
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 180;
            Vector3 rot = _turret[i].eulerAngles;
            rot.z = angle;
            _turret[i].eulerAngles = rot;
        }

    }
}
