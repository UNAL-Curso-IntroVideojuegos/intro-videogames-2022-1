using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _fr = 1f;
    private float timer = 0f;
    void Update()
    {
        timer -= Time.deltaTime;
        // Presionar el botón de disparo por defecto
        if (Input.GetButton("Fire1") && timer <= 0){
            GameObject projectile = Instantiate(_projectilePrefab); // Crear una instancia del proyectil (Esto esta asignado mediante el SerializeField)
            projectile.transform.position = _shootPoint.position; // Se le asigna la posición de inicio
            projectile.transform.rotation = _shootPoint.rotation; // Se le asigna la rotación
            timer = _fr;
        }
    }
}
