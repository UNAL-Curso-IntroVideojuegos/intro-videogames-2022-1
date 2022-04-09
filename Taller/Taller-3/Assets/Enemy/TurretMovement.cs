using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{   
    //Principal object (Enemy)
    public GameObject objectToMove;

    //Pivots points
    public GameObject startPoint;
    public GameObject endPoint; 

    public float speed = 0.5f;

    //Objects for the aim of the turrests
    public GameObject player;
    public GameObject turret1;
    public GameObject turret2;


    // Start is called before the first frame update
    void Start(){    }

    // Update is called once per frame
    void Update()
    {        
        //Move the principal object (enemy) between the points
        objectToMove.transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, Mathf.PingPong(Time.time * speed, 1.0f));

        /*  Aim to the player*/

        //First torret
        Vector3 dir1 = player.transform.position - turret1.transform.position;
        dir1.Normalize();
        float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;

        Vector3 rot1 = turret1.transform.eulerAngles; 
        rot1.z = angle1 + 90f;
        turret1.transform.eulerAngles = rot1; 

        //Second turret
        Vector3 dir2 = player.transform.position - turret2.transform.position;
        dir2.Normalize();
        float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;

        Vector3 rot2 = turret2.transform.eulerAngles; 
        rot2.z = angle2 + 90f; 
        turret2.transform.eulerAngles = rot2;
    }

}
