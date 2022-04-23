using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]    private GameObject _projectilePrefab;
    [SerializeField]    private Transform _shootPoint;
    [SerializeField]    private float fireRate = 1f;

                        private float _timer = 0;

    void Start(){ }

    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //Creación disparo
    private void Shoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = _shootPoint.position;
        projectile.transform.rotation = _shootPoint.rotation;

    }
}
