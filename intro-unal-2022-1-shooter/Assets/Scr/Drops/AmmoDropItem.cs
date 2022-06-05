using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDropItem : DropItem
{
    [SerializeField]
    [Range(1, 100)]
    private int _ammo = 5;
    
    protected  override void OnCollected(PlayerMovement player)
    {
        if (player.TryGetComponent(out PlayerShooter shooter))
        {
            shooter.AddExtraAmmo(_ammo);
        }
    }
}
