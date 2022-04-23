using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // Declaring a Class Variable for the Prefab of the Bullet
    [SerializeField] 
    private GameObject _projectilePrefab;
    
    // Declaring a Class Variable for the Transform of the Shoot Point
    [SerializeField] 
    private Transform _shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Identifies if the User is pressing the Left Click Button
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate make copies of the Bullet once its Prefab has been created
            GameObject projectile = Instantiate(_projectilePrefab);
            
            /* Defining the position of the Bullet Copies with respect to the position and
             rotation of the Shoot Point */
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
            
        }   
    }
}
