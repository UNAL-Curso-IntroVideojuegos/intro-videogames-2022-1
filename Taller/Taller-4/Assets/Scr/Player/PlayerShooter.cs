using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bullet_prefab;

    [SerializeField] 
    private Transform _shoot_point;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Shoot 
            GameObject bullet = Instantiate(_bullet_prefab);
            bullet.transform.position = _shoot_point.position;
            bullet.transform.rotation = _shoot_point.rotation;
        }
    }
}
