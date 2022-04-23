using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 2); //destroying prefab after two seconds
    }
}
