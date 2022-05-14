using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAgentConfig 
{
    public EnemyStateType initialState = EnemyStateType.Idle;
    
    [Header("Detection")]
    public float DetectionRange = 5;
    public float ViewAngle = 60;
    
    [Header("Moving")] 
    public float IdleTime = 2;
    public float PathfindingRefreshTime = 1;
    public List<Transform> PathPoints;
    
    [Header("Attack")]
    public float AttackRange = 1;
    public float AttackDuration = 1;
    public float FightTime = 5;
}