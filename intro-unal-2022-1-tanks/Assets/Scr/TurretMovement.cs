using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private Transform _playertank;
    [SerializeField] private Transform _turret;
    private float max = 8;
    private float min = -4;
    private float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Torreta mira a jugador
        Vector3 playerpos = _playertank.position;
        Vector3 turretaim = playerpos - transform.position;
        float angle = Mathf.Atan2(turretaim.y, turretaim.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
        
        //Movimiento del tanque 
        Vector3 pos = transform.position;
        if (pos.y > max || pos.y < min) { dir *= -1; }
        pos.y += Time.deltaTime * speed * dir;
        transform.position = pos;
        
    }
}