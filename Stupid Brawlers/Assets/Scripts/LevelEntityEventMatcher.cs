using System;

public class LevelEntityEventMatcher : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly LevelDispatcher _levelDispatcher;
    private readonly RewardCoordinator _rewardCoordinator;

    public LevelEntityEventMatcher(LevelContext levelContext, LevelDispatcher levelDispatcher, RewardCoordinator rewardCoordinator)
    {
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
        _rewardCoordinator = rewardCoordinator;
    }    
    
    public void MatchPlayer(PlayerView playerView, UIContainer uiContainer)
    {
        playerView.GunView.OnShoot += _levelDispatcher.DispatchPlayerWantsToShoot;
        playerView.GunView.OnAllBulletsFinished += _levelDispatcher.DispatchAllBulletsFinished;
        playerView.GunView.BulletCount.OnValueChanged += uiContainer.BulletField.UpdateBulletCount;
    }

    public void MatchEnemy(Enemy enemy)
    {
        enemy.OnDied += _levelDispatcher.DispatchEnemyDied;
    }
    
    public void MatchUI(UIContainer uiContainer)
    {
        _levelDispatcher.OnLevelCompleted += uiContainer.ShowCompleteWindow;
        _rewardCoordinator.OnRewardAssigned += uiContainer.SetRewardScore;
    }
    
    public void Dispose()
    {
        _levelContext.Player.GunView.OnShoot -= _levelDispatcher.DispatchPlayerWantsToShoot;
        _levelContext.Player.GunView.OnAllBulletsFinished -= _levelDispatcher.DispatchAllBulletsFinished;
        _levelContext.Player.GunView.BulletCount.OnValueChanged -= _levelContext.UI.BulletField.UpdateBulletCount;

        foreach (var enemy in _levelContext.Enemies) 
            enemy.OnDied -= _levelDispatcher.DispatchEnemyDied;
        
        _levelDispatcher.OnLevelCompleted -= _levelContext.UI.ShowCompleteWindow;
        _rewardCoordinator.OnRewardAssigned -= _levelContext.UI.SetRewardScore;
    }
}