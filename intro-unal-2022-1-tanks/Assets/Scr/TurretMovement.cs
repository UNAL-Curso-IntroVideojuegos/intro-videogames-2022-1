using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 40f;
    [SerializeField]
    private Transform _turret;
    [SerializeField] 
    private Transform _playerTank;
    [SerializeField]
    private Transform _enemyTank;
    private float max = 4;
    private float min = -2;
    private float direccion = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento de la torreta siguiendo el tanque
        Vector3 playerPosition = _playerTank.position;
        Vector3 aimVector = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 270;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;

        //Mocimiento del tanque enemigo en el eje y
        Vector3 pos = _enemyTank.position;
        if (pos.y > max || pos.y < min) { 
            direccion = direccion * (-1); 
            }
        pos.y += Time.deltaTime * speed * direccion;
        _enemyTank.position = pos;

    }
}