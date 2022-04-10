using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{

    public float speed;
    //public Transform playerTarget;
    public string move;
    public float RotationSpeed;
    //public Vector3 position;

    public Transform BlackTank;
    public Transform Turrent;
    
    private Quaternion _lookRotation;
    private Vector3 _direction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        move = "subir";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _direction = (Vector3.zero - BlackTank.position).normalized;

        float angulo = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 270;
        
        Debug.Log(angulo);
        
        Vector3 rotar = Turrent.eulerAngles;
        
        rotar.z = angulo;
        Turrent.eulerAngles = rotar;
        
        
        if (transform.localPosition.y >= 4)
        {
            move = "bajar";
        }
        
        else if (transform.localPosition.y <= -2)
        {
            move = "subir";
        }
        
        if (move == "subir")
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            //Turrent.Rotate(0, 0, 1);
        }
        
        else 
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            //Turrent.Rotate(0, 0, 1);
        }    
    }
}
