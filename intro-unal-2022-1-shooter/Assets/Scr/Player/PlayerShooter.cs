using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    [SerializeField]
    private float fireRate = 0.7f;
    private float _timer = 0;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private Transform _shootPoint;

    void Start()
    { }

    //Disparar
    void Update()
    {
        bool shoot = Input.GetButton("Fire1");

        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, fireRate);
        if(_timer == 0 && shoot)
        {
            Debug.Log("Shot!");
            Shoot();
            _timer = fireRate;
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