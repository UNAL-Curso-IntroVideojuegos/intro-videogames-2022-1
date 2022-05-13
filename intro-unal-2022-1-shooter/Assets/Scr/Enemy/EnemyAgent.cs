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
        //If Target is less than 5 mt
        Vector3 targetRealtivePosition = _target.position - transform.position;

        bool isInRange = targetRealtivePosition.magnitude < AgentConfig.DetectionRange;

        bool isInVision = Vector3.Angle(transform.forward, targetRealtivePosition) < AgentConfig.ViewAngle;

        return isInRange && isInVision;
    }

    private void OnDrawGizmos()
    {
        float halfFOV = AgentConfig.ViewAngle / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRayDirection * AgentConfig.DetectionRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * AgentConfig.DetectionRange);
    }
}
