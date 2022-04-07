using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float distance = 3;
    [SerializeField]
    private Transform _turret;
    [SerializeField] 
    private Transform _playerTank;
    private float direction = 1;
    private float movedDistance = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPosition = _playerTank.position;
        Vector3 aimVector = playerPosition - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;


        if (movedDistance > distance)
        {
            // Alternar el sentido de la dirección
            direction *= -1;
            // Re inicializar el contador de movimiento
            movedDistance = 0;
        }

        movedDistance += speed * Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime * transform.up;
    }
}
