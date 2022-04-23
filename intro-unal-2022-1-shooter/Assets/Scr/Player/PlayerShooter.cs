using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform Weapon;
    [SerializeField] private GameObject bullet;

    private float timer = 1.5f; // timer for Fire-Rate
    private float timer2 = 0f; // timer for continuous shooting
    private float timer3 = 0.3f; // timer for continuous shooting


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //shot
        if(Input.GetButtonDown("Fire1")){
            shot();
            }
        
        // challenge
        // Fire-Rate
        timer -= Time.deltaTime; 
        if(timer <= 0){
            shot();
            timer = 1.5f;
        }

        // challenge
        // continuous shooting
        timer2 = timer2 + Time.deltaTime;
        if(Input.GetButton("Fire1") && timer2>timer3){
            shot();
            timer2=0;
        }
        

    }

    private void shot(){
        GameObject bala2 = Instantiate(bullet, Weapon.position, Weapon.rotation);;
        bala2.transform.forward = Weapon.forward;
    }
}
