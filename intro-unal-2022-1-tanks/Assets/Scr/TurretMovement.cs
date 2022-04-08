using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private Transform[] _turrets;
    [SerializeField]
    private Transform _playerTank;
    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;

    private float normalizeTime;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        normalizeTime = Time.time * speed / Vector3.Distance(_pointA.position, _pointB.position); //Normalizamos el tiempo para que la velocidad del enemigo sea constante sin importar que tan lejos estén los puntos A y B

        transform.position = Vector2.Lerp(_pointA.position, _pointB.position, Mathf.PingPong(normalizeTime, 1)); //Actualizamos la posición del enemigo con ayuda de la función Learp 

        Vector3 playerPosition = _playerTank.position;

        foreach (Transform turret in _turrets)  //Iteramos sobre las torretas que tenga el enemigo y actualizamos la rotación de cada una
        {
            Vector3 aimVector = playerPosition - turret.position;
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

            Vector3 rot = turret.eulerAngles;
            rot.z = angle;
            turret.eulerAngles = rot;
        }
        
    }
}
