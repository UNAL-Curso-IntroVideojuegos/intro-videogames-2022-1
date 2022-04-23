using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform point;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = point.position;
            projectile.transform.rotation = point.rotation;
            projectile.transform.forward = point.forward;
        }
    }
}