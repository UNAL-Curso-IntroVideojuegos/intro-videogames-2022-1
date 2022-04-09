using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private float moveSpeed; //speed
    private float direction; //direction control
    private GameObject target; //target (blue tank)
    private Transform barrel; //barrel

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //setting fps
        moveSpeed = 0.05f; //setting speed value
        direction = 1f; //setting starting directions
        target = GameObject.Find("Tank");  //getting the blue tank GameObject
        barrel = transform.GetChild(0); //getting the barrel
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y  + moveSpeed*direction); //tank displacement
        if (transform.position.y >= 1.93 && direction == 1) direction = -1; //changing direction (backwards)
        else if (transform.position.y <= -1.93 && direction == -1) direction = 1; //changind direction (upwards)
        //Process of barrel tracking the blue tank
        Vector3 aimVector =  target.transform.position - transform.position; 
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = barrel.eulerAngles;
        rot.z = angle;
        barrel.eulerAngles = rot;
    }

}