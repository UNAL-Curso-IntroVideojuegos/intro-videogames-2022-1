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

    private bool _isClose;
    private bool _inFront;
    private float _timer;
    
    void Start()
    {
        _pathFindingController = GetComponent<PathFindingController>();
        _stateMachineController = new StateMachineController();
        _stateMachineController.Init(this);
        
        _target = GameObject.FindWithTag("Player").transform;
        _timer = 0;
    }
    
    void Update()
    {
        _stateMachineController.OnUpdate();
        
        _animator.SetBool("IsMoving", !_pathFindingController.IsStopped);


        //_animator.SetTrigger("Attack", false);
        
    }
    
    public bool IsLookingTarget()
    {
        //PRIMER PUNTO
        _isClose =(_target.position - transform.position).magnitude < AgentConfig.DetectionRange;
        _inFront = Vector3.Dot(_target.position,transform.forward) >= 0;
        return _isClose && _inFront;
    }

    public bool AttackTarget()
    {
        _isClose =(_target.position - transform.position).magnitude < AgentConfig.AttackRange;
        _inFront = Vector3.Dot(_target.position,transform.forward) >= 0;
        return _isClose && _inFront;
    }
}
