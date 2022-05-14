using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Camera _cam;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cam = Camera.main;
    }

    void Update()
    {
        bool isStopped = _agent.isStopped || 
                         (_agent.remainingDistance <= _agent.stoppingDistance && _agent.velocity.sqrMagnitude == 0);

        //if (isStopped)
        if(Input.GetMouseButtonDown(0)) //Si oprimi click izq
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                _agent.SetDestination(hitInfo.point);
            }
        }
        
        //Si efectivamente complete el recorrido
        if (!_agent.pathPending && isStopped)
        {
        }
    }
}
