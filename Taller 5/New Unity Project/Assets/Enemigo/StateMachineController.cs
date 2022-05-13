using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMachineController
{
    private Dictionary<EnemyStateType, IEnemyState> _states;
    private EnemyAgent _agent;
    private EnemyStateType _currentState = EnemyStateType.Idle;
    
    public void Init(EnemyAgent agent)
    {
        _agent = agent;
        _states = new Dictionary<EnemyStateType, IEnemyState>();
        _states.Add(EnemyStateType.Idle, new EnemyIdleState());
        _states.Add(EnemyStateType.Chase, new EnemyChaseState());
        _states.Add(EnemyStateType.Attack, new EnemyAttackState());
        _states.Add(EnemyStateType.Patrol, new EnemyPatrolState());
        ChangeToState(_agent.AgentConfig.initialState);
    }
    
    public void OnUpdate()
    {
        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnUpdate(_agent); //Tick
        }
    }
    public void ChangeToState(EnemyStateType newState)
    {
        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnExit(_agent);
        }
        
        _currentState = newState;
        if (_states.ContainsKey(_currentState))
        {
            IEnemyState state = _states[_currentState];
            state.OnEnter(_agent);
        }
    }
}