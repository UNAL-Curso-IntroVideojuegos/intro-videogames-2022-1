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
    private float dot = 0.0f;
    
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
    
    public bool  IsLookingTarget()
    {
        //If Target is less than 5 mt
       
        
        //aqui se calcula si el player esta en el campo de visión
        //para la tarea simplemente es dejar el angulo de vision en 180
        //para el reto es ajustar el angulo hasta formar el cono deseado
        Vector3 pos = _target.position - transform.position;
        float distance = pos.magnitude;
        pos.Normalize();
        float dotPov = Mathf.Cos(AgentConfig.ViewAngle*0.5f*Mathf.Deg2Rad);
        dot = Vector3.Dot(transform.forward,pos);
        if (distance <= AgentConfig.DetectionRange && dot >= dotPov ){
            return true;
        }
        else{
            return false;
        }
        
        
        //return (_target.position - transform.position).magnitude < AgentConfig.DetectionRange;
    }



    //Funcion simplemente para graficar
    //Estara en verde si no esta en su rango de vision y en rojo si lo esta
    private void OnDrawGizmos() {
        Vector3 pos = _target.position - transform.position;
        float distance = pos.magnitude;
        pos.Normalize();
        float dotPov = Mathf.Cos(AgentConfig.ViewAngle*0.5f*Mathf.Deg2Rad);
        dot = Vector3.Dot(transform.forward,pos);
        
        
        if (distance <= AgentConfig.DetectionRange && dot >= dotPov ){
            Gizmos.color = Color.red;
        }
        else{
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(transform.position, _target.position);
        Vector3 angleA = PointAngle(AgentConfig.ViewAngle/2,false);
        Vector3 angleB = PointAngle(-AgentConfig.ViewAngle/2, false);
        Gizmos.DrawLine(transform.position, transform.position+angleA*AgentConfig.DetectionRange);
        Gizmos.DrawLine(transform.position, transform.position+angleB*AgentConfig.DetectionRange);

        
    
    }

    private Vector3 PointAngle(float angle,bool global ){
        if (!global){
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad),0,Mathf.Cos(angle*Mathf.Deg2Rad));
    }

    
}
