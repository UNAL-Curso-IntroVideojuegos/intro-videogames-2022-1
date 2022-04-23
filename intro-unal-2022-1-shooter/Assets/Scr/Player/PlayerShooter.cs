using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    private float timer = 2.0f;
    private float myTime = 0.0F;
    private float fireDelta = 0.5F;
    private float nextFire = 0.5F;

    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        
        timer -= Time.deltaTime; 
        if(timer <= 0){
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPoint.position;
            projectile.transform.rotation = shootPoint.rotation;
            projectile.transform.forward = shootPoint.forward;
            timer = 2.0f;
        }

        // Continuous shooting
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Fire1") && myTime > nextFire){
            // Shoot
            nextFire = myTime + fireDelta;
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPoint.position;
            projectile.transform.rotation = shootPoint.rotation;
            projectile.transform.forward = shootPoint.forward;

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }
}