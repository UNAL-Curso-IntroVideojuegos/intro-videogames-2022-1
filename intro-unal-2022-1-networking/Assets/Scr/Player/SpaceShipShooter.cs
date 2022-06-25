using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpaceShipShooter : NetworkBehaviour
{
    [SerializeField]
    private Projectile _projectilePrefab;

    [Space(20)]
    [SerializeField] 
    private float _timeBetweenShoots = 0.25f;
    [SerializeField]
    private Transform _spawnPointParent;
    
    private SpaceShipController _shipController;
    
    private float _nextShootAt;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _shipController = GetComponent<SpaceShipController>();
        
        if (IsOwner)
        {
            _shipController.ShipInput.OnWeaponShoot += Shoot;
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        
        if (IsOwner)
        {
            _shipController.ShipInput.OnWeaponShoot += Shoot;
        }
    }

    private void Update()
    {
        if (!IsOwner)
            return;
    }

    //Client
    private void Shoot()
    {
        if (Time.time < _nextShootAt)
            return;

        if(!IsServer) //Only for clients
            _nextShootAt = Time.time + _timeBetweenShoots;
        
        ShootServerRpc();
    }


    [ServerRpc]
    private void ShootServerRpc()
    {
        if (Time.time < _nextShootAt)
            return;

        _nextShootAt = Time.time + _timeBetweenShoots;

        if (_spawnPointParent.childCount == 0)
        {
            SpawnBullet(_spawnPointParent.position, _spawnPointParent.rotation);
        }
        else
        {
            foreach (Transform spawnPoint in _spawnPointParent)
            {
                SpawnBullet(spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        if(!IsServer)
            return;
        
        Projectile go = Instantiate(_projectilePrefab, position, rotation);
        go.GetComponent<NetworkObject>().Spawn();
        go.SetPlayer(_shipController);
    }
}
