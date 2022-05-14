
public enum EnemyStateType
{
    Idle,
    Chase,
    Patrol,
    Attack
}

public interface IEnemyState
{
    public void OnEnter(EnemyAgent agent);
    public void OnUpdate(EnemyAgent agent);
    public void OnExit(EnemyAgent agent);
}