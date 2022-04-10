using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float radio = 4f;
    [SerializeField] private float _timer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] shootPoints;
    [SerializeField] private Transform area;

    void Start(){
        // Se crea el circulo con el area de disparo al jugador
        area.transform.localScale = new Vector3(radio*1.5f, radio*1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = (player.position - transform.position).magnitude;
        // Verificacion de distancia entre enemigo y jugador
        if (distancia < radio){
            // Disminucion del timer para disparar cada cierto tiempo
            _timer -= Time.deltaTime; 
            if (_timer <= 0) {
                // Disparo de proyectil en cada cañon
                foreach (Transform shootPoint in shootPoints)
                {
                    GameObject projectile = Instantiate(projectilePrefab);
                    projectile.transform.position = shootPoint.position;
                    projectile.transform.rotation = shootPoint.rotation;
                }
                _timer = 1f;
            }
        }
        
    }
}
