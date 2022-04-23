using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    void Start()
    {
        
    }
    void Update() {
if (Input.GetButtonDown("Fire1")){
    GameObject projectile = Instantiate(projectilePrefab);
    projectile.transform.position = shootPoint.position;
    projectile.transform.rotation = shootPoint.rotation;
    projectile.transform.forward = shootPoint.forward;
    }
}
}
