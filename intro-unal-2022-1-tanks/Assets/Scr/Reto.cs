using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reto : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform min;
    [SerializeField]
    private Transform max;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private Vector3 dir;
    [SerializeField]
    private Transform tank;
    
    [SerializeField]
    private Transform turret;
    [SerializeField]
    private Transform turret2;
    [SerializeField]
    private Transform[] _turretArray;
    void Start()
    {
        _turretArray = new Transform[2] {turret,turret2};
        dir = max.position;
        
    }

    // Update is called once per frame
    void Update()
    {   
        // mover hacia los dos objetos
        Vector3 pos = transform.position;
        pos = Vector3.MoveTowards(pos, dir, speed*Time.deltaTime);
        transform.position = pos;

        if (transform.position == max.position){
            dir = min.position;
        }
        if (transform.position == min.position){
            dir = max.position;
        }
        

        //hacer que la torreta mire al tanke
        Debug.Log(_turretArray.Length);
        for (int i = 0; i <_turretArray.Length; i++ ){
            Debug.Log(i);
            Vector3 tankPos = tank.position;
            Vector3 aimVector = tankPos - transform.position;
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
            
            Vector3 rot = _turretArray[i].eulerAngles;
            rot.z = angle;
            _turretArray[i].eulerAngles = rot;
        }
        
        
        

        

    }
}
