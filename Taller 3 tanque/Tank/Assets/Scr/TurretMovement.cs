using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public float MaximoY = 4;
    public float MinimoY = -2;
    private float Sentido = 1;
    public float Speed = 1;
    private Transform _Tank;
    private Transform _canyon; 

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.y > MaximoY || pos.y < MinimoY) { Sentido *= -1; }
        pos.y += Time.deltaTime * Speed * Sentido;
        transform.position = pos;

        Vector3 Tank = _Tank.position;
        Vector3 aimVector = Tank - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = _canyon.eulerAngles;
        rot.z = angle;
        _canyon.eulerAngles = rot;
    }
}



