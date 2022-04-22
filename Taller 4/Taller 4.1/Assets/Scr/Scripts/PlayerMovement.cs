using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public class PlayerMovement : MonoBehaviour

{
    private Rigidbody _rb;
    private Vector3 _velocity;

    
    public float VelociDsh = 3;
    public float  TimeDsh = 3;
    public float Time =0;
    public int Dsh = 0;
    private Vector3 dirDsh;

    void Start()
    {
    _rb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
    float Horizontal = Input.GetAxisRaw("Horizontal");
    float Vertical = Input.GetAxisRaw("Vertical");

    Vector3 _dir  = new Vector3(Horizontal, 0, Vertical);
    _dir.Normalize();
    dirDsh = _dir;
    _velocity = VelociDsh * _dir;
    if (Dsh == 0)
    {
    if (Input.GetKeyDown(KeyCode.LeftShift))
   {
    Dsh = 1;
   }
    }
    else
  {
    if (Time >= TimeDsh)
 {
    Dsh = 0;
    Time = 0;
 }
    else
  {
   Time += Time.deltaTime;
  }
    }
    }
}




    

    

