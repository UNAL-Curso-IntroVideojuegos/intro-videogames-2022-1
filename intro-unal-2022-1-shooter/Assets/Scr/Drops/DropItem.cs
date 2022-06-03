using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    void Collect()
    {
        OnCollected();
        Destroy(gameObject);
    }

    protected virtual void OnCollected() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            Collect();
        }
    }
}
