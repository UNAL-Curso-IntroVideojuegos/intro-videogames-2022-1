using System;

public static class GameEvents
{
    public static Action OnStartScreenEvent;
    public static Action OnStartGameEvent;
    public static Action<int, bool, float, int> OnGameOverEvent; //total score

    public static Action<int> OnLevelProgressEvent;
    public static Action<int> OnScoreUpdated;
    
    public static Action<EnemyAgent, int> OnEnemyDeathEvent;
    public static Action<int> OnPlayerHealthChangeEvent;
    public static Action<int> OnPlayerAmmoUpdatedEvent;

    public delegate void OnBonus(int points);
    public static OnBonus OnBonusCollectedEvent;
}
