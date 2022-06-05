using UnityEngine;

public class PointDropItem : DropItem
{
    [SerializeField]
    [Range(1, 100)]
    private int _bonusPoints = 20;
    
    protected  override void OnCollected(PlayerMovement player)
    {
        GameEvents.OnBonusCollectedEvent?.Invoke(_bonusPoints);
    }
}
