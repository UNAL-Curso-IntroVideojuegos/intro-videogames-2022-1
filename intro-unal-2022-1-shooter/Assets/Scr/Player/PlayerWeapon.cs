using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    
    [SerializeField]
    private Transform shootPoint;

    [SerializeField] private float fireRate = 2;
    
    private float recharge = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (recharge > 0)
        {
            recharge = Math.Min(fireRate, recharge + Time.deltaTime);
            if (Math.Abs(recharge - fireRate) < 0.01)
            {
                recharge = 0;
            }
        }
        if (Input.GetButton("Fire1") && recharge == 0)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPoint.position;
            projectile.transform.rotation = shootPoint.rotation;
            recharge = 0.1f;
        }
    }
}
