using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
  private float _NewPoint = 0.1f;
  private int _ActualPoint = 0;
  public void OnEnter(EnemyAgent agent)
  {
    Debug.Log("Patrol: OnEnter");
  }
  
  public void OnUpdate(EnemyAgent agent)
  {
    Debug.Log("Patrol: OnUpdate");

    Vector3 distance = agent.AgentConfig.PathPoints[_ActualPoint].position - agent.transform.position;

    if (agent.IsLookingTarget())
    {
      agent.StateMachineController.ChangeToState(EnemyStateType.Chase);
    }
    else if (distance.magnitude < _NewPoint){

      _ActualPoint ++;
      if(_ActualPoint>= 3){
        _ActualPoint = 0;

      }
      agent.StateMachineController.ChangeToState(EnemyStateType.Idle);
    }
    else {
      agent.PathFindingController.GoTo(agent.AgentConfig.PathPoints[_ActualPoint].position, null);
    }

  }
  
  public void OnExit(EnemyAgent agent)
  {
    Debug.Log("Patrol: OnExit");
  }
}
