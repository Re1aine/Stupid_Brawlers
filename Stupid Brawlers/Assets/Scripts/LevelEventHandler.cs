using System;
using System.Collections;
using UnityEngine;

public class LevelEventHandler : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly RewardCoordinator _rewardCoordinator;
    private readonly PopupMaster _popUpMaster;
    private readonly ICoroutineExecutor _coroutineExecutor;
    private readonly LevelDispatcher _levelDispatcher;
    
    private readonly float _delayLevelCompletedEvent = 5f;
    
    public LevelEventHandler(LevelContext levelContext,
        RewardCoordinator rewardCoordinator,
        PopupMaster popUpMaster,
        ICoroutineExecutor coroutineExecutor,
        LevelDispatcher levelDispatcher)
        
    {
        _levelContext = levelContext;
        _rewardCoordinator = rewardCoordinator;
        _popUpMaster = popUpMaster;
        _coroutineExecutor = coroutineExecutor;
        _levelDispatcher = levelDispatcher;
    }

    public void Run()
    {
        _levelDispatcher.OnEnemyDied += HandleOnEnemyDiedEvent;
        _levelDispatcher.OnLevelCompleted += HandleOnLevelCompletedEvent;
        _levelDispatcher.OnAllBulletsFinished += HandleAllBulletsFinished;
    }

    public void Dispose()
    {
        _levelDispatcher.OnEnemyDied -= HandleOnEnemyDiedEvent;
        _levelDispatcher.OnLevelCompleted -= HandleOnLevelCompletedEvent;
        _levelDispatcher.OnAllBulletsFinished -= HandleAllBulletsFinished;
    }

    private void HandleOnEnemyDiedEvent(Enemy enemy)
    {
        _levelContext.RemoveEnemy(enemy);
        _rewardCoordinator.AssignRewardForEnemy(enemy);
        _popUpMaster.CreatePopUpAt(enemy);
        
        if (_levelContext.Enemies.Count > 0)
            return;
        
        _coroutineExecutor.StartCoroutine(HandleDelayedOnLevelCompletedEvent());
    }

    private IEnumerator HandleDelayedOnLevelCompletedEvent()
    {
        yield return new WaitForSeconds(_delayLevelCompletedEvent);
        _levelDispatcher.DispatchLevelCompleted();
    }

    private void HandleOnLevelCompletedEvent()
    {
        _levelContext.Player.LockInput();

        foreach (var shootedBullet in _levelContext.ShootedBullets)
            if (shootedBullet != null)
                shootedBullet.FreezeMove();
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> LEVEL COMPLETED <color=red>");
    }
    
    private void HandleAllBulletsFinished()
    {
        if (_levelContext
                .Player
                .GunView
                .GetBulletCount() > 0) return;
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  " <color=red> BULLETS ARE FINISHED <color=red>");
    }
}