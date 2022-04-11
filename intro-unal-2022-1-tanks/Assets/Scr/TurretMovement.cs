using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _point1;
    [SerializeField]
    private Transform _point2;
    [SerializeField]
    private float min;
    [SerializeField]
    private float max;
    [SerializeField]
    private Transform _turret;
    [SerializeField]
    private Transform _target;
    private float timeValue = 0.0f;
    private float xInitialPosition;

    void Start(){
        xInitialPosition = transform.position.x;
    }
    
    
    void Update()
    {
        
        //Movement

        Vector3 pos = transform.position;

        // Compute the sin position.
        float amplitud = Mathf.Abs(max-min);
        float xValue = amplitud/2 * Mathf.Sin(timeValue * 2.0f) + xInitialPosition;
        Debug.Log(xValue);
        // Now compute the Clamp value.

        // Update the position of the cube.
        pos =transform.position;
        pos = transform.right * (xValue) + transform.up * pos.y;
        transform.position = pos;


        // Increase animation time.
        timeValue = timeValue + Time.deltaTime;

        // Reset the animation time if it is greater than the planned time.
        if (timeValue * 2.0f > Mathf.PI* 2)
        {
            timeValue = 0.0f;
        }


        //Turrent movement

        Vector3 aimVector = _target.position - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        
    }

}
