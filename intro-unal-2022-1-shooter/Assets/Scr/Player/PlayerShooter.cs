using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    //referencing the spawn point
    private GameObject spawnPoint;
    
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint"); //instantiating the spawn point for the gun
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //getting the bullet
			GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"), spawnPoint.transform.position, Quaternion.identity);
            //making the shot
			bullet.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward*20, ForceMode.Impulse);
        }
    }
}
