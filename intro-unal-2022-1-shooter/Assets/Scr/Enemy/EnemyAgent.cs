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
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = _target.position - transform.position;
        if ((_target.position - transform.position).magnitude < AgentConfig.DetectionRange && Vector3.Dot(forward, toOther) > 0)
        {
            return true;
        }
        return false;
        //If Target is less than 5 mt
        //return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange &&
            //Vector3.Dot(transform.position, _target.transform.position) > 0;//AND is looking at player
    }
}
