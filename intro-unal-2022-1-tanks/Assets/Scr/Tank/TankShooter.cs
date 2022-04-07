using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;
    void Update()
    {
        // Presionar el botón de disparo por defecto
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(_projectilePrefab); // Crear una instancia del proyectil (Esto esta asignado mediante el SerializeField)
            projectile.transform.position = _shootPoint.position; // Se le asigna la posición de inicio
            projectile.transform.rotation = _shootPoint.rotation; // Se le asigna la rotación
        }
    }
}
