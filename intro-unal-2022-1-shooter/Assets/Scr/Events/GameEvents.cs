using System;

public static class GameEvents
{
    public static Action<int> OnEnemyDeathEvent;
    public static Action<int> OnPlayerHitEvent;
    
    public delegate void OnBonus(int points);
    public static OnBonus OnBonusCollectedEvent;
}
