using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    
    private float timeshoot = 0.5F;
    private float timer = 2.0f;
    private float deltashoot = 0.5F;
    private float time = 0.0F;

    [SerializeField] private GameObject projectile1;
    [SerializeField] private Transform firePoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        time = time + Time.deltaTime;
        
        if (Input.GetButton("Fire1") && time > timeshoot){
            // Shoot
            timeshoot = time + deltashoot;
            GameObject projectile = Instantiate(projectile1);
            
            projectile.transform.position = firePoint.position;
            
            projectile.transform.rotation = firePoint.rotation;
            
            projectile.transform.forward = firePoint.forward;

            timeshoot = timeshoot - time;
            time = 0.0F;
            
        
        timer -= Time.deltaTime; 
        
        if(timer <= 0){
            GameObject projectile = Instantiate(projectile1);
            
            projectile.transform.position = firePoint.position;
            
            projectile.transform.rotation = firePoint.rotation;
            
            projectile.transform.forward = firePoint.forward;
            
            timer = 2.0f;
        }



        }
    }
}
