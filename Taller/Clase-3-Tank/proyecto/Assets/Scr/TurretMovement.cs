using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = (float)0.2;
    
    [SerializeField]
    private Transform _turret;
    private float min_pos_y = (float) -1.85;
    private float max_pos_y = (float) 1.64;
    int direction = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Movimiento del tanque
        
        Vector3 pos = transform.position;
        float next_pos = pos.y + direction * (float) (speed * Time.deltaTime);
        pos.y = Mathf.Clamp(next_pos, min_pos_y, max_pos_y);

        if (pos.y == min_pos_y)
        {
            direction = 1;
        }
        else if (pos.y == max_pos_y)
        {
            direction = -1;
        }
        transform.position = pos;
        
        // Movimiento del cañón
        
        Vector3 player_tank_position = GameObject.Find("Tank_RB").transform.position;
        Vector3 aimVector = player_tank_position - pos;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;
        
        Vector3 turret_rotation = _turret.eulerAngles;
        turret_rotation.z = 90 + angle;
        _turret.eulerAngles = turret_rotation;
    }
}
