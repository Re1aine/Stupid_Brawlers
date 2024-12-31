using System;

public class LevelDispatcher
{
    public event Action OnLevelStarted;
    public event Action OnLevelCompleted;
    public event Action<Enemy> OnEnemyDied;
    public event Action OnPlayerShooted; 
    public event Action OnAllBulletsFinished;

    public void DispatchLevelStarted() => OnLevelStarted?.Invoke();
    public void DispatchEnemyDied(Enemy enemy) => OnEnemyDied?.Invoke(enemy);
    public void DispatchLevelCompleted() => OnLevelCompleted?.Invoke();
    public void DispatchAllBulletsFinished() => OnAllBulletsFinished?.Invoke();
    public void DispatchPlayerWantsToShoot() => OnPlayerShooted?.Invoke();
    
}