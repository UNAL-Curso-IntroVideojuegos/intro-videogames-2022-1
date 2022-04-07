using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2;
    private Rigidbody2D _rb;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>(); // Asigna el RigidBody 2D del objeto a la variable _rb
        _rb.velocity = transform.up * _speed; // transform.up es (0, 1, 0), aquí se le esta asignado la velocidad en el eje Y del objeto
    }

    private void OnCollisionEnter2D(Collision2D other) // En la variable other obtenemos el objeto al que chocó
    {
        // Debug.Log("Collision with " + other.transform.name);

        // Enemy enemy = other.transform.GetComponent<Enemy>();
        // if (enemy){
        //     enemy.TakeDamage();
        // }

        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)){
            enemy.TakeDamage();
        }
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // gameObject.SetActive(false); // Desactiva el objeto pero sigue existiendo
        Destroy(gameObject); // Elimina el objeto
    }
}
