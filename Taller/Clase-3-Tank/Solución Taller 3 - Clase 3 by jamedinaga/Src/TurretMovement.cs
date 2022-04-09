using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour

{
    // Inputs for the Enemy's movement
    public float maxX = -1;
    public float minX = -6;
    public float speed = 2;
    private bool m_DirRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Definition of the range in which the Enemy is going to move
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, minX, maxX), position.y,
            position.z);
        transform.position = position;

        if (m_DirRight)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
        }

        else
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0);
        }


        if (transform.position.x >= maxX)
        {
            m_DirRight = false;
        }

        if (transform.position.x <= minX)
        {
            m_DirRight = true;
        }
    }
}
