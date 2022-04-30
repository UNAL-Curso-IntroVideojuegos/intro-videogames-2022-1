using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFindingController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Action _onArrive;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        bool isStopped = _agent.isStopped || 
                         (_agent.remainingDistance <= _agent.stoppingDistance && _agent.velocity.sqrMagnitude == 0);
        
        //Si efectivamente complete el recorrido
        if (!_agent.pathPending && isStopped)
        {
            if (_onArrive != null)
            {
                _onArrive();
                _onArrive = null;
            }
        }
    }

    public void GoTo(Vector3 position, Action callback)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
        _onArrive = callback;
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }
}
