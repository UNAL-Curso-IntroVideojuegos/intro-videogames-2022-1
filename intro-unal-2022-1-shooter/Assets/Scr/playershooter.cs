using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _firerate = 2.0f;

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Shoot
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
            //projectile.transform.up = _shootPoint.up;
        }
    }
}