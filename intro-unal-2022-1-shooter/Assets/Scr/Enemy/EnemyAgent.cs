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
        return Vector3.Dot(toOther, forward) > 0 &&
               toOther.magnitude < AgentConfig.DetectionRange;
    }

    public void Rot()
    {
        Vector3 aimVector = transform.position - _target.position;
        float angle = Mathf.Atan2(aimVector.x, aimVector.z) * Mathf.Rad2Deg - 90;
        Debug.Log("Angle: " + angle);

        Vector3 rot = transform.eulerAngles;
        // rot.y = angle;
        // transform.eulerAngles = rot;
    }
}
