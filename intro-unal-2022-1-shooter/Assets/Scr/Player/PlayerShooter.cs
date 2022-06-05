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
    [SerializeField]
    private int _maxBaseAmmo = 10; 
    [SerializeField]
    private float _reloadTime = 2; //Bullet per second
    
    [Space(20)]
    [SerializeField]
    private AudioSource _audioSource;

    private PlayerAnimation _playerAnimation;
    private float _fireTimer = 0;

    private bool _isReloading = false;
    private int _remainingAmmo;
    private float _reloadingTimer = 0;
    private int _extraAmmo = 0;
    private int _totalAmmo = 0;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        Reset();
    }

    void Update()
    {
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }

        if (_reloadingTimer > 0)
        {
            _reloadingTimer -= Time.deltaTime;
        }

        if (_isReloading)
        {
            if (_reloadingTimer <= 0)
            {
                Reload();
            }
            return;
        }
        
        if (_remainingAmmo > 0)
        {
            if (_fireTimer <= 0 && Input.GetButton("Fire1"))
            {
                //To get the time between each bullet we do  1 / rate
                _fireTimer = 1 / _rateFire;
                Shoot();
            }
        }
        
        if (!_isReloading && _remainingAmmo < _totalAmmo && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R)))
        {
            _isReloading = true;
            _reloadingTimer = _reloadTime;
            AudioManager.Instance.PlaySound2D("Reload");
        }
        
        _playerAnimation.SetIsShooting(_fireTimer > 0);
        _playerAnimation.SetIsReloading(_isReloading);
    }

    public void Reset()
    {
        _extraAmmo += 0;
        _totalAmmo = _maxBaseAmmo + _extraAmmo;
        Reload();
    }
    
    private void Reload()
    {
        _reloadingTimer = 0;
        _isReloading = false;
        _remainingAmmo = _totalAmmo;
        GameEvents.OnPlayerAmmoUpdatedEvent?.Invoke(_remainingAmmo);
    }

    public void AddExtraAmmo(int ammo)
    {
        _extraAmmo += ammo;
        _totalAmmo = _maxBaseAmmo + _extraAmmo;
        _remainingAmmo += _extraAmmo;
        GameEvents.OnPlayerAmmoUpdatedEvent?.Invoke(_remainingAmmo);
    }
    
    private void Shoot()
    {
        _remainingAmmo--;
        
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
        GameEvents.OnPlayerAmmoUpdatedEvent(_remainingAmmo);
    }
    
}