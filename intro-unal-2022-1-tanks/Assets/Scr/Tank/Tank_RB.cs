using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_RB : MonoBehaviour
{
    // Para los SerializeField se asigna su valor desde Unity
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _velocity;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Transform _turret; // Transform es la parte de la posición, rotación y escala de un objeto

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Se genera un valor para las teclas de movimiento, ya sea -1, 0 (Sin presionar), 1
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Se obtiene la posición del mouse desde el centro del mundo
        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;

        Vector3 aimVector = mouseWorldPos - transform.position; // Vector del punto (0, 0) a la posición del mouse, menos vector del punto (0, 0) a la posición del objeto, esto nos da el vector al cual tiene que apuntar el objeto desde su posición
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90; // Convertimos en vector anterior a un angulo y hacemos correción de 90°
        Vector3 rot = _turret.eulerAngles; // Creamos un objeto auxiliar para no modificar directamente a la torreta 
        rot.z = angle; // Cambiamos su angulo de mira
        _turret.eulerAngles = rot;

        // Se calcula la velocidad del cuerpo normalizando la presión de las teclas
        _velocity = new Vector2(horizontal, vertical);
        _velocity.Normalize();
        _velocity = _velocity * speed;
    }

    private void FixedUpdate()
    {
        // Se le asigna la velocidad al cuerpo
        _rb.velocity = _velocity;
    }

    // Funciones para detectar colisión
    // private void OnCollisionEnter2D(Collision2D other){
    //     Debug.Log("Collision with " + other.transform.name);
    // }
    // private void OnCollisionStay2D(Collision2D other){
    //     Debug.LogWarning("Stay with " + other.transform.name);
    // }
    // private void OnCollisionExit2D(Collision2D other){
    //     Debug.LogAssertion("Exit from " + other.transform.name);
    // }

}
