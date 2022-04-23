using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _Bullet;
    [SerializeField]
    private Transform _shootPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject Bullet = Instantiate(_Bullet);
            Bullet.transform.position = _shootPoint.position;
            Bullet.transform.rotation = _shootPoint.rotation;
            Bullet.transform.forward = _shootPoint.forward;
            Destroy(Bullet, 5f);
        }
    }
}