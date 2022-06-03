using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAgentConfig 
{
    public int Points = 10;
    public EnemyStateType initialState = EnemyStateType.Idle;

    [Header("Detection")] 
    public bool GlobalDetection = false;
    public float DetectionRange = 5;
    public float ViewAngle = 60;
    
    [Header("Moving")] 
    public float IdleTime = 2;
    public float PathfindingRefreshTime = 1;
    public List<Transform> PathPoints;
    
    [Header("Attack")]
    public float AttackRange = 1;
    public float AttackDuration = 1;
    public int AttackDamage = 10;

    [Header("Death")] 
    public Transform DeathVFXPoint;
    public GameObject DeathVFX;
    
    [Header("Drops")]
    [Min(0)]
    public int NumberOfDrops;
    public EnemyDropItem[] DropItems;
}

[Serializable]
public class EnemyDropItem
{
    public DropItem ItemPrefab;
    [Range(0,1)]
    public float Probability;
}