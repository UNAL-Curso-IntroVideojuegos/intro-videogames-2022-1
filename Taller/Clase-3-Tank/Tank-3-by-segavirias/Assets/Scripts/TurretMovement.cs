using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private float moveSpeed;
    private bool direction;
    [SerializeField]
    private GameObject target;
    private Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        barrel = GameObject.Find("specialBarrel3_outline").transform;
        moveSpeed = 1.5f*Time.deltaTime;
        direction = true;   
    }

    // Update is called once per frame
    void Update()
    {
       int sign = direction ? 1 : -1;
       transform.position = new Vector3(transform.position.x, transform.position.y  + moveSpeed*sign);
       if (transform.position.y >= 1.5) direction = false;
       else if (transform.position.y <= -1.5) direction = true; 

       Vector3 aimVector = target.transform.position - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = barrel.eulerAngles;
        rot.z = angle;
        barrel.eulerAngles = rot;
    }

}
