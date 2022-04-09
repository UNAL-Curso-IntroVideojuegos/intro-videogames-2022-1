using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretMovement : MonoBehaviour
{
    private float i = 0;
    private float fSpeed=0.75F; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 X = transform.position;
        Quaternion Z = transform.rotation;
        X.y = Convert.ToSingle(2 + 3*Math.Cos(i));
        Z.z = Convert.ToSingle(Math.Atan((X.y-0.27)/(5.4))*180/3.14);
        i+=Time.deltaTime*fSpeed;
        transform.position = X;
        transform.rotation = Quaternion.Euler(Z.x, Z.y, Z.z-90);
    }
}
