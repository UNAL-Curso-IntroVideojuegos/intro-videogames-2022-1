using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : LivingEntity
{
    [Space(20)]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private EnemyAgentConfig _agentConfig;
    [SerializeField] 
    private int _points = 10;

    private Collider _collider;
    private PathFindingController _pathFindingController;
    private StateMachineController _stateMachineController;
    private Transform _target;
    private EnemyHalthBar _halthBar;

    private Vector3 _initPosition;
    
    public EnemyAgentConfig AgentConfig => _agentConfig;
    public Transform Target => _target;
    public Animator Animator => _animator;
    public Collider Collider => _collider;
    public PathFindingController PathFindingController => _pathFindingController;
    public StateMachineController StateMachineController => _stateMachineController;
    
    protected override void OnInit()
    {
        base.OnInit();

        _collider = GetComponent<Collider>();
        _pathFindingController = GetComponent<PathFindingController>();
        _halthBar = GetComponent<EnemyHalthBar>();
        _stateMachineController = new StateMachineController();
        _stateMachineController.Init(this);
        
        //_target = GameObject.FindWithTag("Player").transform;
        _target = GameManager.Instance.Player;

        _initPosition = transform.position;

        base.OnTakeDamage = OnTakeDamageCallback;
        
        Debug.Log("Enemy Start");
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
        //return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange;
        
        Vector3 direction = Target.position - transform.position;
        
        //Solo adelante
        // float dotProduct = Vector3.Dot(direction.normalized, transform.forward);
        // if (dotProduct > 0f && direction.magnitude <= AgentConfig.DetectionRange)
        // {
        //     return true;
        // }
        
        float distance = direction.magnitude;
        if (distance <= AgentConfig.DetectionRange)
        {
            float angleToPlayer = Vector3.Angle(direction, transform.forward);
            if (angleToPlayer < AgentConfig.ViewAngle / 2)
                return true;
        }

        return false;
    }

    void OnTakeDamageCallback(int damage)
    {
        _halthBar.UpdateHealthBar(_health, _totalHealth);
    }
    
    [ContextMenu("Death")]
    protected override void OnDeath()
    {
        base.OnDeath();
        
        _stateMachineController.ChangeToState(EnemyStateType.Death);
        
        //GameEvents.OnEnemyDeath?.Invoke();
        if (GameEvents.OnEnemyDeathEvent != null)
        {
            GameEvents.OnEnemyDeathEvent.Invoke(_points);
        }
        
        //gameObject.SetActive(false);
    }

    public override void Reset()
    {
        base.Reset();
        
        Init();
        
        _stateMachineController.ChangeToState(EnemyStateType.Idle);
        _halthBar.UpdateHealthBar(_health, _totalHealth);
        transform.position = _initPosition;
        _collider.enabled = true;
        gameObject.SetActive(true);
    }
}
