using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Transform _torreta;
    [SerializeField]
    private Transform _tanque;
    [SerializeField] 
    private float Limsup = 5;
    [SerializeField] 
    private float Liminf = -5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Movimiento punto 2
        Vector3 Location = transform.position;
        if (Mathf.Clamp(Location.y, Liminf, Limsup) ==  Limsup)
        {
            speed = -1*speed;

        } else if (Mathf.Clamp(Location.y, Liminf, Limsup) ==  Liminf)
        {
            speed=-1*speed;
        }
        Location.y += speed * Time.deltaTime;
        transform.position = Location;
         // Rotacion punto 3
        
        Vector3 aimCannon = _tanque.position - transform.position;
        float angulo = Mathf.Atan2(aimCannon.y, aimCannon.x) * Mathf.Rad2Deg + 90;
        Vector3 aim = _torreta.eulerAngles;
        aim.z=angulo;
        _torreta.eulerAngles = aim;

        


    
    }
}
