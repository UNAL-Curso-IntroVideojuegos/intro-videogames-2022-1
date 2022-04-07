using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _velocity;

    // Campos para el movimiento estatico
    // [SerializeField]
    // private float top = 5;
    // [SerializeField]
    // private float bottom = -2;
    // private int dir = 1;

    [SerializeField]
    private Transform[] _turrets;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform[] _positions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento de Enemy Estatico
        // top = Mathf.Clamp(top, -4, 8);
        // bottom = Mathf.Clamp(bottom, -4, 8);
        // if(transform.position.y > top){
        //     dir = -1;
        // } else if (transform.position.y < bottom) {
        //     dir = 1;
        // }
        // _velocity = new Vector2(0, dir);
        // _velocity.Normalize();
        // _velocity = _velocity * speed;

        // Movimiento de Enemy con Puntos
        float dist = Vector3.Distance(_positions[0].transform.position, _positions[1].transform.position);
        Vector3 interpolatedPosition = Vector3.Lerp(_positions[0].transform.position, _positions[1].transform.position, Mathf.PingPong(Time.time*speed/dist, 1));
        transform.position = interpolatedPosition;

        // Rotacion del barrel (Solo para un barrel dentro de _turrets[])
        // Forma 1
        // Vector3 aimVector = _turrets[0].transform.position - _player.transform.position;
        // float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

        // Forma 2
        // Vector3 aimVector = (_player.transform.position - _turrets[0].transform.position).normalized;
        // float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;

        // Vector3 rot = _turrets[0].eulerAngles; 
        // rot.z = angle;
        // _turrets[0].eulerAngles = rot;

        // Rotación para varias torretas NO FUNCIONA ADECUADAMENTE, HACE MOVIMIENTOS EN 3D
        // _turrets[0].up = _turrets[0].transform.position - _player.transform.position;
        // _turrets[1].up = _turrets[1].transform.position - _player.transform.position;
        // Lo mismo pero con un foreach
        // foreach (Transform t in _turrets) {
        //     t.up = t.transform.position - _player.transform.position;
        // }

        // Rotación adecuada para varias torretas
        foreach (Transform t in _turrets) {
            Vector3 aimVector = (_player.transform.position - t.transform.position).normalized;
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg + 90;
            Vector3 rot = t.eulerAngles; 
            rot.z = angle;
            t.eulerAngles = rot;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
    }
}
