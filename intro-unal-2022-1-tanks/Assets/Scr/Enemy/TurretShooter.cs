using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    private float _range = 4f;
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform[] _shootPoints;
    [SerializeField]
    private Transform _tank;
    private float _timer;
    [SerializeField]
    private Transform _radio;

    void Start(){
        float d = _range*2;
        _radio.transform.localScale = new Vector3(d, d, 1f); // Se asigna el diametro al area de fuego
    }

    // Update is called once per frame
    void Update(){
        float dist = Vector3.Distance(_tank.transform.position, transform.position); // Se obtiene la distancia del Enemy al Tank
        _timer -= Time.deltaTime; // Se va reduciendo el _timer
        if (dist < _range){ // Cuando esta dentro del rango y el timer es menor a 0 puede disparar
            if(_timer <= 0){
                foreach(Transform sp in _shootPoints){
                    GameObject projectile = Instantiate(_projectilePrefab); // Crear una instancia del proyectil (Esto esta asignado mediante el SerializeField)
                    projectile.transform.position = sp.position; // Se le asigna la posición de inicio
                    projectile.transform.rotation = sp.rotation; // Se le asigna la rotación
                }
                _timer = 2f;
            }
        }
    }
}
