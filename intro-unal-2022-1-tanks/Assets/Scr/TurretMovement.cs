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
    private Transform[] _turrets;
    [SerializeField]
    private Transform _target;
    private float timeValue = 0.0f;
    private float xInitialPosition;
    private float yInitialPosition;

    void Start(){
        xInitialPosition = transform.position.x;
        yInitialPosition = transform.position.y;
    }
    
    
    void Update()
    {
        /*
        ///////Movement con puntos fijos

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

        */

        
        //Movimiento con dos puntos
        float t = Mathf.Abs(Mathf.Sin(timeValue));
        Vector3 posiblePosition =Vector3.Lerp(_point1.position, _point2.position, t);
        timeValue = timeValue + Time.deltaTime/5;

        transform.position = posiblePosition;
         if (timeValue * 2.0f > Mathf.PI* 2)
        {
            timeValue = 0.0f;
        }
        
        //Turrents movement


        for(int i = 0;i<=1; i++){
            Vector3 aimVector = _target.position - _turrets[i].position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        
        Vector3 rot = _turrets[i].eulerAngles;
        rot.z = angle;
        _turrets[i].eulerAngles = rot;
        }
        
    }

}
