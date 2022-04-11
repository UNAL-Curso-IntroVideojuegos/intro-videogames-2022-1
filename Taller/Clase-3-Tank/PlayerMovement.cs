using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Saber si el usuario oprimió flecha Izq. o Der.
        //Condición: Usuario oprimió <- o ->
        
        //Derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Lo que debe pasar si la condición se cumple: Mueva el Pj
            
            //Move el Pj a la izq. o a la der.
            // Pj -> Transform -> Position -> X + 10
            Vector3 newPosition = transform.position;
            newPosition.x = newPosition.x + speed * Time.deltaTime;
            transform.position = newPosition;
        }
        
        //Izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Lo que debe pasar si la condición se cumple: Mueva el Pj
            
            //Move el Pj a la izq. o a la der.
            // Pj -> Transform -> Position -> X + 10
            Vector3 newPosition = transform.position;
            newPosition.x = newPosition.x - speed * Time.deltaTime;
            transform.position = newPosition;
        }
        
        //Arriba
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Lo que debe pasar si la condición se cumple: Mueva el Pj
            
            //Move el Pj a la izq. o a la der.
            // Pj -> Transform -> Position -> X + 10
            Vector3 newPosition = transform.position;
            newPosition.y = newPosition.y + speed * Time.deltaTime;
            transform.position = newPosition;
        }
        
        //Abajo
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Lo que debe pasar si la condición se cumple: Mueva el Pj
            
            //Move el Pj a la izq. o a la der.
            // Pj -> Transform -> Position -> X + 10
            Vector3 newPosition = transform.position;
            newPosition.y = newPosition.y - speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
