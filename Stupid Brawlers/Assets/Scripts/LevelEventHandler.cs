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

    private readonly LevelInformer _levelInformer;
    private readonly LevelSaveLoadMaster _levelSaveLoadMaster;

    private readonly float _delayLevelCompletedEvent = 5f;

    public LevelEventHandler(LevelContext levelContext,
        RewardCoordinator rewardCoordinator,
        ICoroutineExecutor coroutineExecutor,
        LevelDispatcher levelDispatcher,
        LevelInformer levelInformer,
        LevelSaveLoadMaster levelSaveLoadMaster)
        
    {
        _levelContext = levelContext;
        _rewardCoordinator = rewardCoordinator;
        _coroutineExecutor = coroutineExecutor;
        _levelDispatcher = levelDispatcher;
        _levelInformer = levelInformer;
        _levelSaveLoadMaster = levelSaveLoadMaster;
    }

    public void Run()
    {
        _levelDispatcher.OnLevelStarted += HandleLevelStarted;
        _levelDispatcher.OnLevelCompleted += HandleOnLevelCompletedEvent;
        _levelDispatcher.OnEnemyDied += HandleOnEnemyDiedEvent;
        _levelDispatcher.OnAllBulletsFinished += HandleAllBulletsFinished;
    }

    private void HandleLevelStarted()
    {
        _levelInformer.SetHighScore(_levelSaveLoadMaster.GetValue(_levelInformer.GetLvlKey(), 0));

        _levelContext.UI.InformMenu.SetHighScore(_levelInformer.GetHighScore());
        
        _levelContext.UI.InformMenu.ClosePauseMenu(1);

        _levelContext.Player.Input.Run();


        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> LEVEL PROGRESS LOADED <color=red>");
        
        Debug.Log("HIGHSCORE ON LEVEL - " + _levelSaveLoadMaster.GetValue(_levelInformer.GetLvlKey(), 0));
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> LEVEL STARTED <color=red>");
    }

    public void Dispose()
    {
        _levelDispatcher.OnLevelStarted -= HandleLevelStarted;
        _levelDispatcher.OnLevelCompleted -= HandleOnLevelCompletedEvent;
        _levelDispatcher.OnEnemyDied -= HandleOnEnemyDiedEvent;
        _levelDispatcher.OnAllBulletsFinished -= HandleAllBulletsFinished;
    }

    private void HandleOnEnemyDiedEvent(Enemy enemy)
    {
        _levelContext.RemoveEnemy(enemy);
        _rewardCoordinator.AssignRewardForEnemy(enemy);
        _levelContext.PopupMaster.CreatePopUpAt(enemy);
        
        if (_levelContext.Enemies.Count > 0)
            return;
        
        _coroutineExecutor.StartCoroutine(HandleDelayedOnLevelCompletedEvent());
    }

    private IEnumerator HandleDelayedOnLevelCompletedEvent()
    {
        _levelContext.Player.Input.Lock();
        
        yield return new WaitForSeconds(_delayLevelCompletedEvent);
        
        _levelDispatcher.DispatchLevelCompleted();
    }

    private void HandleOnLevelCompletedEvent()
    {
        if (_levelInformer.GetHighScore() < _rewardCoordinator.GetAllRewardValue()) 
            _levelSaveLoadMaster.SetValue(_levelInformer.GetLvlKey(), _rewardCoordinator.GetAllRewardValue());

        foreach (var shootedBullet in _levelContext.ShootedBullets)
            if (shootedBullet != null)
                shootedBullet.FreezeMove();

        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> LEVEL PROGRESS SAVED <color=red>");
            
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> LEVEL COMPLETED <color=red>");
    }
    
    private void HandleAllBulletsFinished()
    {
        if (_levelContext
                .Player
                .GunView
                .BulletCount
                .Value > 0) return;
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  " <color=red> BULLETS ARE FINISHED <color=red>");
    }
}