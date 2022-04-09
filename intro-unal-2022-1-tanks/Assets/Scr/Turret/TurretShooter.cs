using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    
    [SerializeField]
    private float _radius = 1;
    [SerializeField]
    private float _rateFire = 1;
    
    [Space(20)]
    [SerializeField]
    private Transform _projectilePrefab;
    [SerializeField]
    private Transform[] _spawnPoints;

    [Space(20)] 
    [SerializeField]
    private Transform _radiusImage;
    
    
    private Transform _target;
    private float _fireTimer = 0;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (_target == null)
        {
            return;
        }
        
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }

        if ((transform.position - _target.position).sqrMagnitude <= _radius * _radius)
        {
            if (_fireTimer <= 0)
            {
                _fireTimer = 1 / _rateFire;
                Shoot();
            }
        }
    }
    
    private void Shoot()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            Transform projectile = Instantiate(_projectilePrefab);
            projectile.position = spawnPoint.position;
            projectile.rotation = spawnPoint.rotation;
        }
    }

    private void OnValidate()
    {
        if (_radiusImage != null)
        {
            _radiusImage.localScale = _radius * 2f * Vector3.one;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
