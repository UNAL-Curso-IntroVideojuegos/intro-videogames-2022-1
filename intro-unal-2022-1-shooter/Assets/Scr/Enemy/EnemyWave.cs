using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Wave")]
public class EnemyWave : ScriptableObject
{
    public bool IsInfinite;
    public EnemyWaveItem[] EnemiesItems;
    public float DelayToSpawn;
    public float TimeBetweenSpawns;
    public int NumberOfSpawns;
}


[System.Serializable]
public struct EnemyWaveItem
{
    public EnemyAgent EnemyPrefab;
    [Range(0,1)]
    public float Probability;
}
