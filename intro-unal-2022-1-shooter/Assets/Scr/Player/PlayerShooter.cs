using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _pointShoot;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _pointShoot.position;
            projectile.transform.rotation = _pointShoot.rotation;

        } 
    }
}
