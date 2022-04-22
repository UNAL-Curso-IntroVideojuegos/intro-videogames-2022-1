using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject projectilePre;
    private float timeShot = 1;
    private float timeShotStart = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shot1();

        }
        //Reto
        if(Input.GetButton("Fire1")){
            Shot2();
        }
    }

    private void Shot1(){
            GameObject proyectile = Instantiate(projectilePre);
            proyectile.transform.position = spawnPoint.position;
            proyectile.transform.rotation = spawnPoint.rotation;
            //Destruir en 5seg
            Destroy(proyectile,5);  
    }
    private void Shot2(){
        if (Time.time > timeShotStart){
                GameObject proyectile = Instantiate(projectilePre);
                proyectile.transform.position = spawnPoint.position;
                proyectile.transform.rotation = spawnPoint.rotation;
                //Destruir en 5seg
                Destroy(proyectile,5); 
                timeShotStart = Time.time + timeShot;

            }

    }
}
