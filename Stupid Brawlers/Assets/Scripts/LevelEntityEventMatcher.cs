using System;

public class LevelEntityEventMatcher : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly LevelDispatcher _levelDispatcher;
    private readonly RewardCoordinator _rewardCoordinator;

    public LevelEntityEventMatcher(LevelContext levelContext,
        LevelDispatcher levelDispatcher,
        RewardCoordinator rewardCoordinator)
    {
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
        _rewardCoordinator = rewardCoordinator;
    }

    public void Run()
    {
        MatchEnemy();
        MatchPlayer();
        MatchUI();
    }
    
    private void MatchPlayer()
    {
        _levelContext.Player.GunView.OnShoot += _levelDispatcher.DispatchPlayerWantsToShoot;
        _levelContext.Player.GunView.OnAllBulletsFinished += _levelDispatcher.DispatchAllBulletsFinished;
        
    }

    private void MatchEnemy()
    {
        foreach (var e in _levelContext.Enemies) 
            e.OnDied += _levelDispatcher.DispatchEnemyDied;
    }

    private void MatchUI()
    {
        _levelDispatcher.OnLevelCompleted += _levelContext.UI.ShowCompleteWindow;
        _rewardCoordinator.OnRewardAssigned += _levelContext.UI.SetRewardScore;
        _levelContext.UI.BulletField.InitBulletCount(_levelContext.Player.GunView.BulletCount);
    }
    
    public void Dispose()
    {
        _levelContext.Player.GunView.OnShoot -= _levelDispatcher.DispatchPlayerWantsToShoot;
        _levelContext.Player.GunView.OnAllBulletsFinished -= _levelDispatcher.DispatchAllBulletsFinished;

        foreach (var enemy in _levelContext.Enemies) 
            enemy.OnDied -= _levelDispatcher.DispatchEnemyDied;
        
        _levelDispatcher.OnLevelCompleted -= _levelContext.UI.ShowCompleteWindow;
        _rewardCoordinator.OnRewardAssigned -= _levelContext.UI.SetRewardScore;
        _levelContext.UI.BulletField.Dispose();        
    }
}