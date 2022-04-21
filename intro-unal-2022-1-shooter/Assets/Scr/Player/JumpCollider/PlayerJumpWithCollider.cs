using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpWithCollider : MonoBehaviour
{
    
    [Header("Jump")]
    [SerializeField]
    private float _jumpHeight = 2;
    [SerializeField]
    private float _customGravity = -20;

    private Rigidbody _rb;
    private GroundDetector _groundDetector;
    private Vector3 _velocity;
    
    //Jump
    private bool _wantToJump;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundDetector = GetComponentInChildren<GroundDetector>();
    }
    
    void Update()
    {
        _wantToJump = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        
        //Apply gravity manual
        _velocity.y = _rb.velocity.y;
        _velocity.y += _customGravity * Time.fixedDeltaTime;
        
        if (_groundDetector.IsGrounded && _wantToJump)
        {
            //V = -2 * g * H -> Kinematic equations
            // Check https://youtu.be/v1V3T5BPd7E for more info!
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _customGravity);
        }
        
        //Apply velocity to RigidBody. An alternative it's to use AddForce
        _rb.velocity = _velocity;
    }
    
}
