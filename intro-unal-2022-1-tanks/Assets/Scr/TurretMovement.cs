using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cannon;
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
        
        //Movimiento del tanque 
        Vector3 pos = transform.position;
        if (pos.y > max || pos.y < min) { dir *= -1; }
        pos.y += Time.deltaTime * speed * dir;
        transform.position = pos;

        //Torreta mira a jugador
        Vector3 player = _player.position;
        Vector3 aimVector = player - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
        Vector3 rot = _cannon.eulerAngles;
        rot.z = angle;
        _cannon.eulerAngles = rot;
    }
}
