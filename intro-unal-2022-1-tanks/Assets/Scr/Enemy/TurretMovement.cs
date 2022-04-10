using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    
    [SerializeField] public Transform jugador;
    [SerializeField]  public Transform[] turrets;
    [SerializeField] private Transform point1; // Objeto P1
    [SerializeField] private Transform point2; // Objeto P2
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento de Torreta entre la posicion de dos objetos vacíos point1 (P1) y point2 (P2)
        transform.position = Vector3.Lerp (point1.position, point2.position, Mathf.PingPong(Time.time*speed, 1.0f));

        // Torretas apuntan al jugador
        Vector3 dir = jugador.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        for (int i = 0; i < turrets.Length; i++)
        {
            Vector3 rot = turrets[i].eulerAngles; // Se obtiene el angulo actual
            rot.z = angle; // Se modifica en z que da la rotacion
            turrets[i].eulerAngles = rot; // Se reasigna
        }

    }
}
