using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private Transform _shootPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
            Destroy(projectile, 3);
        }
    }
}
