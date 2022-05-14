using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    //Declaring a Class variable for the coordinates of the next point on the patrol path
    private Vector3 nextPoint;
    
    //Declaring a Class variable to change between the path points
    private int _currentPointIndex = 0;
    
    public void OnEnter(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnEnter");
        
        //Calling the function that returns the next point on the patrol path
        nextPoint = GetAgentNextPoint(agent);
        
        //Defining the Enemy movement to that point
        agent.PathFindingController.GoTo(nextPoint,null);
    }

    public void OnUpdate(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnUpdate");
        
        //Determining how far the Enemy is from the next point on the patrol path
        float distanceToPathPoint = (nextPoint - agent.transform.position).magnitude;
        
        //If the Enemy is close to the point, make the transition to the Idle state for a few seconds
        if (distanceToPathPoint < 0.1f)
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
        }
        
        //Vi al Player -> Pasar al estado Chase! 
        if (agent.IsLookingTarget())
        {
            agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
        }
    }

    public void OnExit(EnemyAgent agent)
    {
        Debug.Log("Patrol: OnExit");
        agent.PathFindingController.Stop();
    }
    
    //Function that returns the next point on the patrol path
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
}
