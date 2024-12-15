using System;

public class LevelEventBinder : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly LevelDispatcher _levelDispatcher;
    private readonly RewardCoordinator _rewardCoordinator;

    public LevelEventBinder(LevelContext levelContext, LevelDispatcher levelDispatcher, RewardCoordinator rewardCoordinator)
    {
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
        _rewardCoordinator = rewardCoordinator;
    }    
    
    public void BindPlayer(PlayerView playerView)
    {
        playerView.GunView.OnShoot += _levelDispatcher.DispatchPlayerWantsToShoot;
        playerView.GunView.OnAllBulletsFinished += _levelDispatcher.DispatchAllBulletsFinished;
    }

    public void BindEnemy(Enemy enemy)
    {
        enemy.OnDied += _levelDispatcher.DispatchEnemyDied;
    }

    private void BindBullet(Bullet bullet)
    {
        bullet.OnDestroyed += _levelContext.RemoveShootedBullet;
    }

    public void BindUI(UIContainer uiContainer)
    {
        _levelDispatcher.OnPlayerShooted += uiContainer.BulletField.SubtractBullet;
        _levelDispatcher.OnLevelCompleted += uiContainer.ShowCompleteWindow;
        _rewardCoordinator.OnRewardAssigned += uiContainer.SetRewardScore;
    }
    
    public void Dispose()
    {
        foreach (var enemy in _levelContext.Enemies) 
            enemy.OnDied -= _levelDispatcher.DispatchEnemyDied;
        
        _levelContext
            .Player
            .GunView.OnShoot -= _levelDispatcher.DispatchPlayerWantsToShoot;
        
        _levelContext
            .Player
            .GunView.OnAllBulletsFinished -= _levelDispatcher.DispatchAllBulletsFinished;
    }
}