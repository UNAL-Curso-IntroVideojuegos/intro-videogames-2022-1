using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private EnemyAgentConfig _agentConfig;
    
    private PathFindingController _pathFindingController;
    private StateMachineController _stateMachineController;
    private Transform _target;
    
    public EnemyAgentConfig AgentConfig => _agentConfig;
    public Transform Target => _target;
    public Animator Animator => _animator;
    public PathFindingController PathFindingController => _pathFindingController;
    public StateMachineController StateMachineController => _stateMachineController;
    
    void Start()
    {
        _pathFindingController = GetComponent<PathFindingController>();
        _stateMachineController = new StateMachineController();
        _stateMachineController.Init(this);
        
        _target = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        _stateMachineController.OnUpdate();
        
        _animator.SetBool("IsMoving", !_pathFindingController.IsStopped);
        //_animator.SetTrigger("Attack");
    }
    
    public bool IsLookingTarget()
    {
        //Punto1
        //Adelante entre 0 y 1. Atras entre 0 y -1
        /*
        Vector3 dirPlayer = (_target.position - transform.position).normalized;
        float testValue = Vector3.Dot(transform.forward, dirPlayer);
        return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange && (testValue >= 0 && testValue <= 1);
        */

        //Reto: Punto1
        Vector3 dirPlayer = (_target.position - transform.position).normalized;
        float testAngle = Vector3.Angle(transform.forward, dirPlayer);
        return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange && (testAngle <= AgentConfig.ViewAngle/2);
    }
}
