using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    void Collect(PlayerMovement player)
    {
        OnCollected(player);
        Destroy(gameObject);
    }

    protected virtual void OnCollected(PlayerMovement player) { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            Collect(player);
            
            //TODO: Add sfx
        }
    }
}
