using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private Transform _turret;
    [SerializeField] 
    private Transform _playerTank;
    [SerializeField]
    private Transform _tallerTank;
    private float ymax = 3;
    private float ymin = -3;
    private float dir = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 playerPosition = _playerTank.position;
        Vector3 aimVector = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        Vector3 pos = _tallerTank.position;
        if (pos.y > ymax || pos.y < ymin) {dir = dir * (-1); //Esto podría causar problemas?
            }
        pos.y = pos.y + Time.deltaTime * speed * dir;
        _tallerTank.position = pos;

    }
}