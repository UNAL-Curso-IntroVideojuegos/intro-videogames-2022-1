using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float min;
    [SerializeField]
    private float max;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float dir = 1;
    [SerializeField]
    private Transform tank;
    [SerializeField]
    private Transform _turret;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // mover de arriba a bajo, de min a max el tanke
        Vector3 pos = transform.position;
        if (pos.y == max){
            dir = -1;
        }
        if (pos.y == min){
            dir = 1;
        }
        pos.y += Time.deltaTime*speed*dir;
        pos = new Vector3(pos.x,Mathf.Clamp(pos.y,min,max));
        transform.position = pos;
        

        //hacer que la torreta mire al tanke
        Vector3 tankPos = tank.position;
        
        Vector3 aimVector = tankPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        
        
        

        

    }
}
