using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float distance;
    private int _currentPointIndex = 0;
    private Vector3 movingTo;
    private float idleTimer = 0;
    public void OnEnter(EnemyAgent agent)
    {
        movingTo = GetAgentNextPoint(agent);
    }

    public void OnExit(EnemyAgent agent)
    {
        
    }

    public void OnUpdate(EnemyAgent agent)
    {
        agent.PathFindingController.GoTo(movingTo, null);
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
        distance = (movingTo - agent.transform.position).magnitude;
        if (distance < 0.1f)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > 2)
            {
                movingTo = GetAgentNextPoint(agent);
                idleTimer = 0;
            }
            

        }

    }
    private Vector3 GetAgentNextPoint(EnemyAgent agent)
    {
        //If we don't have a Path return (0, 0, 0)
        if (agent.AgentConfig.PathPoints == null || agent.AgentConfig.PathPoints.Count == 0)
        {
            return Vector3.zero;
        }

        //Move through the path. When reach the end -> start from 0 again
        _currentPointIndex = (_currentPointIndex + 1) % agent.AgentConfig.PathPoints.Count;
        return agent.AgentConfig.PathPoints[_currentPointIndex].position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
