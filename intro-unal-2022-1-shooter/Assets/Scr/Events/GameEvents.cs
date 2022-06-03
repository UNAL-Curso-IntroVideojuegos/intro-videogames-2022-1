using System;

public static class GameEvents
{
    public static Action OnStartScreenEvent;
    public static Action OnStartGameEvent;
    public static Action OnGameOverEvent; //total score
    
    public static Action<EnemyAgent, int> OnEnemyDeathEvent;
    public static Action<int> OnPlayerHealthChangeEvent;
    public static Action<int> OnPlayerAmmoUpdatedEvent;
    
    
    
    public delegate void OnBonus(int points);
    public static OnBonus OnBonusCollectedEvent;
}
