using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTank;
    [SerializeField] private Transform[] turretCanyon;
    [SerializeField] private Transform maxY;
    [SerializeField] private Transform minY;
    public float speed = 0.4f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.Lerp(minY.position, maxY.position, Mathf.PingPong(Time.time * speed, 1.0f));
        
        Vector3 direction = playerTank.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        for (int i = 0; i < turretCanyon.Length; i++)
        {
            Vector3 rot = turretCanyon[i].eulerAngles;
            rot.z = angle;
            turretCanyon[i].eulerAngles = rot;
        }
    }
}
