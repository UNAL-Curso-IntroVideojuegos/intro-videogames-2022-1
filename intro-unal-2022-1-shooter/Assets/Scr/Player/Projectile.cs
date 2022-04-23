using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Declaring the Class Variable for the Bullet Speed
    [SerializeField]
    private float speed = 10;
    
    // Declaring the Class Variable for the Rigidbody of the GameObject
    private Rigidbody _rb;
    
    // Declaring the Class Variable for the Destroy time of the Bullet
    [SerializeField] 
    private float destroyTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // This function will get the Rigidbody of the Bullet at the beginning of the code
        Init();
    }

    private void Init()
    {   
        // Getting the Rigidbody of the Bullet
        _rb = GetComponent<Rigidbody>();
        
        // Movement of the Bullet
        _rb.velocity = transform.forward * speed;        
    }
    

    // Update is called once per frame
    void Update()
    {
        // Identifies if the User is pressing the Left Click Button
        if (Input.GetButtonDown("Fire1"))
        {
            // Calls the DestroyGameObject function
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        // This function destroys the Game Object of the Bullet after certain amount of time
        Destroy(gameObject, destroyTime);
    }
}
