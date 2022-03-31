using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] public float speed; // Velocidad del enemigo
    [SerializeField] public Transform player; // Referencia a la posición del jugador
    [SerializeField] public Transform[] turrets; // Lista de todas las torretas del tanque
    [SerializeField] public Transform[] waypoints; // Lista de los puntos entre los que se mueve

    private float counter = 0; // Inicializador del contador, no es necesario que empiece en 0, solo que suba

    void Update()
    {
        // Movimiento entre 2 puntos
        counter += speed * Time.deltaTime; // Necesario para que Mathf.PingPong sirva y podamos customizar la velocidad
        float t = Mathf.PingPong(counter, 1); // Valor entre 0 y 1 que primero incrementa y luego decrementa
        transform.position = Vector3.Lerp(waypoints[0].position, waypoints[1].position, t); // Interpolación de 2 vectores

        // Torretas miran al jugador
        Vector2 playerPos = player.position - transform.position;
        playerPos.Normalize();
        float rotacion = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].rotation = Quaternion.Euler(0f, 0f, rotacion - 90);
        }

    }
}
