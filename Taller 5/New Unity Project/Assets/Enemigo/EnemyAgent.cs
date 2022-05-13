using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAgent : MonoBehaviour
{
    
    private Animator _anim;
    private EnemyAgentConfig _ac;
    private PathFindingController _pathFindingController;
    private StateMachineController _stateMachineController;
    private Transform _target;
    
    public EnemyAgentConfig AgentConfig => _ac;
    public Transform Target => _target;
    public Animator Animator => _anim;
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
        _anim.SetBool("IsMoving", !_pathFindingController.IsStopped);
    }
    
    public bool IsLookingTarget()
    {
        return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange;  
        bool cback = (_target.position - transform.position).magnitude < AgentConfig.DetectionRange;
        bool cfront = Vector3.Dot(transform.forward, _target.position - transform.position) > 0;
        return cback && cfront;
    }
}
