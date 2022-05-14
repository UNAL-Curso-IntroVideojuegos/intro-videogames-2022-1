using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFindingController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Action _onArrive;
    public bool IsStopped { get; private set; }

    private bool _wasActiveThisFrame = false;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.isStopped = true;
    }
    
    void Update()
    {
        if (_wasActiveThisFrame)
        {
            _wasActiveThisFrame = false;
            return;
        }
        
        if (_agent.isStopped)
        {
            IsStopped = true;
            return;
        }

        //Si efectivamente completamos el recorrido
        if (_agent.remainingDistance <= _agent.stoppingDistance && _agent.velocity.sqrMagnitude == 0)
        { 
            IsStopped = true;
            Debug.LogError("On Arrive");
            if (_onArrive != null)
            { 
                _onArrive();
                _onArrive = null;
            }
        }
    }

    public void GoTo(Vector3 position, Action callback)
    {
        _wasActiveThisFrame = true;
        _onArrive = callback;
        _agent.SetDestination(position);
        _agent.isStopped = false;
        IsStopped = false;
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }

 
}