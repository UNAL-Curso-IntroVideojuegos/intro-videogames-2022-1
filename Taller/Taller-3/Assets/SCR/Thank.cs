using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thank : MonoBehaviour
{
    //[Serializable]
    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos += speed* Time.deltaTime * transform.up*vertical;
        pos += speed* Time.deltaTime * transform.right*horizontal;

        transform.position = pos;   

        
    }
}
