using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private Transform _shootPoint;
    
    [Space(20)]
    [SerializeField]
    private float _rateFire = 1; //Bullet per second
    
    private float _fireTimer = 0;

    void Update()
    {
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
        
        
        if (_fireTimer <= 0 && Input.GetButton("Fire1"))
        {
            //To get the time between each bullet we do  1 / rate
            _fireTimer = 1 / _rateFire; 
            Shoot();
        }
    }
    
    private void Shoot()
    {
        //Shoot
        GameObject projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = _shootPoint.position;
        projectile.transform.rotation = _shootPoint.rotation;
        //projectile.transform.up = _shootPoint.up;
    }
}