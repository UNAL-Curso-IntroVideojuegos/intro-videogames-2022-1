using System;
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
    private GameObject _shellPrefab;
    [SerializeField]
    private Transform _shellPoint;
    
    [Space(20)]
    [SerializeField]
    private GameObject _muzzleFlash;
    
    
    [Space(20)]
    [SerializeField]
    private float _rateFire = 1; //Bullet per second
    
    [Space(20)]
    [SerializeField]
    private AudioSource _audioSource;

    private PlayerAnimation _playerAnimation;
    private float _fireTimer = 0;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

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
        
        _playerAnimation.SetIsShooting(_fireTimer > 0);
    }
    
    private void Shoot()
    {
        //Shoot
        GameObject projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = _shootPoint.position;
        //projectile.transform.rotation = _shootPoint.rotation;
        Vector3 rot = _shootPoint.eulerAngles;
        rot.x = 0;
        projectile.transform.eulerAngles = rot;
        
        //AudioManager.Instance.PlaySound("GunShoot", projectile.transform.position);
        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        _muzzleFlash.SetActive(true);
        
        Instantiate(_shellPrefab, _shellPoint.position, _shellPoint.rotation);
        
        GameManager.Instance.Camera.StartScreenShake(0.15f, 0.2f);
    }
}