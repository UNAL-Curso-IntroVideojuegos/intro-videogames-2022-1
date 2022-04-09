using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Punto1
    private float timeValue = 0.0f;
    private float yMin = -1.0f, yMax = 1.0f;
    // Punto2
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _turret;
    // Punto2 Reto
    [SerializeField]
    private Transform[] _turrets;
    
    void Start()
    {
        
    }

    void Update()
    {
        // Punto1 
        // yValue entre -1 y 1
        float yValue = Mathf.Sin(timeValue);
        float yPos = Mathf.Clamp(yValue, yMin, yMax);
        // Update the position of the cube.
        transform.position = new Vector3(3.0f, yPos, 0.0f);
        // Increase animation time.
        timeValue += Time.deltaTime;

        // Punto2
        /*
        Vector3 playerWorldPos = _player.position;
        playerWorldPos.z = 0;
        Vector3 aimVector = playerWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        */

        // Punto2 Reto
        Vector3 playerWorldPos = _player.position;
        playerWorldPos.z = 0;
        Vector3 aimVector = playerWorldPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        for (int i = 0; i < _turrets.Length; i++)
        {
            Vector3 rot = _turrets[i].eulerAngles;
            rot.z = angle;
            _turrets[i].eulerAngles = rot;
        }
        
    }
}
