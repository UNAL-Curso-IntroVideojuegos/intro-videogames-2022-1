using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private bool _isGrounded = false;

    public bool IsGrounded => _isGrounded;
    
    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }
}
