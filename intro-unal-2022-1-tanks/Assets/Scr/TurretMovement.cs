using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]    
    private Transform postion1;

    [SerializeField]    
    private Transform position2;

    [SerializeField]
    private Camera _cam;

    [SerializeField]    
    private Transform look;

    [SerializeField]
    private Transform _turret;

    public float speed = 0.5f;

    //private Vector3 direccion;

    void Start()
    {
        //direccion= position2.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(postion1.position, position2.position, Mathf.PingPong(Time.time*speed, 1.0f));

        Vector3 aimVector = (look.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 270;

        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

    }
}