using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]    
    private Transform comienzo;

    [SerializeField]    
    private Transform final;

    [SerializeField]
    private Camera _cam;

    [SerializeField]    
    private Transform mira;

    [SerializeField]
    private Transform _torreta;

    [SerializeField]
    private Transform _torreta2;
    public float velocidad = 0.5f;

    //private Vector3 direccion;

    void Start()
    {
        //direccion= final.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(comienzo.position, final.position, Mathf.PingPong(Time.time*velocidad, 1.0f));
        

        // if(transform.position==comienzo.position){
        //     direccion=final.position;
        // }
        // if(transform.position==final.position){
        //     direccion=comienzo.position;}


        //Vector2 aux = mira.position;
        //Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(aux);
        //mouseWorldPos.z = 0;
        
        Vector3 aimVector = (mira.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _torreta.eulerAngles;
        rot.z = angle;
        _torreta.eulerAngles = rot;

        Vector3 rot2 = _torreta2.eulerAngles;
        rot2.z = angle;
        _torreta2.eulerAngles = rot2;


    }
}

