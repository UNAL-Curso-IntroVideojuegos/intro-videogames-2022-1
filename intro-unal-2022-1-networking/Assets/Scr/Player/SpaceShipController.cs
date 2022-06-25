using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpaceShipController : NetworkBehaviour
{
    private SpaceShipInput _shipInputs;
    private SpaceShipMovement _shipMovement;
    private SpaceShipShooter _shipShooter;

    private NetworkVariable<SpaceShipNetworkState> _playerState;

    //Public getters
    public SpaceShipInput ShipInput => _shipInputs;
    
    private void Awake()
    {
        _shipInputs = GetComponent<SpaceShipInput>();
        _shipMovement = GetComponent<SpaceShipMovement>();
        _shipShooter = GetComponent<SpaceShipShooter>();
        
        _playerState = new NetworkVariable<SpaceShipNetworkState>(writePerm: NetworkVariableWritePermission.Owner);
    }

    private void Update()
    {
        //TODO: Maybe don't do it on every frame?
        _shipInputs.enabled = IsOwner;

        if (IsOwner)
        {
            WriteNetworkData();
        }
        else
        {
            ReadNetworkData();
        }
    }


    private void WriteNetworkData()
    {
        _playerState.Value = new SpaceShipNetworkState {
            Position = _shipMovement.Position,
            Rotation = transform.rotation.eulerAngles
        };
    }
    

    private void ReadNetworkData()
    {
        _shipMovement.SetTargetMovementData(_playerState.Value.Position, _playerState.Value.Rotation);
    }
}
