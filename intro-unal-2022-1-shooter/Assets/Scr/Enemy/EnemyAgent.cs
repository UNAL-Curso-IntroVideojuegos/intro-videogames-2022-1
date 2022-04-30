using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    
    private Transform _target;
    
    public Transform Target => _target;
    
    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        //_animator.SetBool("IsMoving", false);
        //_animator.SetTrigger("Attack");
    }
    
    public bool IsLookingTarget()
    {
        //If Target is less than 5 mt
        return (_target.position - transform.position).magnitude < 5;
    }
}
