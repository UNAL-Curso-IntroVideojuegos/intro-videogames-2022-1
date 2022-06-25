using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpaceShipInput : MonoBehaviour
{
    public Vector2 InputAxis { get; private set; }
    public Action OnWeaponShoot;
    public Action OnBomnbShoot;
    
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (Input.GetMouseButton(0))
            OnWeaponShoot?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.Space))
            OnBomnbShoot?.Invoke();
    }
}

