using System;
using System.Collections;
using UnityEngine;

public class LevelDispatcher  
{
    public event Action OnPlayerShooted;
    public event Action<int> OnAllRewardPointsAssigned; 
    public event Action<int> OnRewardPointsAssigned;
    public event Action OnAllBulletsFinished;
    
    //public event Action OnAllEnemiesDied;

    public event Action<Enemy> OnEnemyDied;
    public event Action OnLevelCompleted; 
    
    private readonly LevelContext _levelContext;
    private readonly PopupMaster _popupMaster;
    //private readonly ICoroutineExecutor _coroutineExecutor;
    private readonly RewardCoordinator _rewardCoordinator;

    //private readonly float _delayDispatch = 5f;

    public LevelDispatcher(LevelContext levelContext,
        RewardCoordinator rewardCoordinator,
        ICoroutineExecutor coroutineExecutor)
    {
        _levelContext = levelContext;
        _rewardCoordinator = rewardCoordinator;
      //  _coroutineExecutor = coroutineExecutor;
    }

    public void DispatchEnemyDied(Enemy enemy)
    {
        //OnRewardPointsAssigned?.Invoke(enemy.GetRewardValue());
        OnEnemyDied?.Invoke(enemy);

        
        //_coroutineExecutor.StartCoroutine(DelayedDispatchAllEnemiesDied());
    }
    
    public void DispatchLevelCompleted()
    { 
        //OnAllRewardPointsAssigned?.Invoke(_rewardCoordinator.GetAllAssignedRewardValue());  
        
        OnLevelCompleted?.Invoke();
    }
    
    //private IEnumerator DelayedDispatchAllEnemiesDied()
    //{
    //    //OnAllRewardPointsAssigned?.Invoke(_rewardCoordinator.GetAllAssignedRewardValue());
    //    
    //    //yield return new WaitForSeconds(_delayDispatch);
    //    
    //    //OnAllEnemiesDied?.Invoke();
    //    
    //}

    public void DispatchAllBulletsFinished()
    {
        if (_levelContext
                .Player
                .GunView
                .GetBulletCount() > 0) return;
        
        OnAllBulletsFinished?.Invoke();
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  " <color=red> BULLETS ARE FINISHED <color=red>");
    }

    public void DispatchPlayerWantsToShoot() => 
        OnPlayerShooted?.Invoke();
    
}

public class LevelEventHandler
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

    public void Subscribe()
    {
        _levelDispatcher.OnEnemyDied += HandleOnEnemyDiedEvent;
        _levelDispatcher.OnLevelCompleted += HandleOnLevelCompletedEvent;
    }

    public void UnSubscribe()
    {
        _levelDispatcher.OnEnemyDied += HandleOnEnemyDiedEvent;
        _levelDispatcher.OnLevelCompleted += HandleOnLevelCompletedEvent;
    }
    
    private void HandleOnEnemyDiedEvent(Enemy enemy)
    {
        _levelContext.RemoveEnemy(enemy);
        _rewardCoordinator.AssignRewardForEnemy(enemy);
        _popUpMaster.CreatePopUpAt(enemy);
        
        
        if (_levelContext.Enemies.Count > 0)
            return;
        
        
        _coroutineExecutor.StartCoroutine(HandleDelayedOnLevelCompletedEvent());
        //_coroutineExecutor.StartCoroutine(HandleOnLevelCompletedEvent());
    }

    private IEnumerator HandleDelayedOnLevelCompletedEvent()
    {
        yield return new WaitForSeconds(_delayLevelCompletedEvent);
        
        _levelDispatcher.DispatchLevelCompleted();
    }
    
    private void HandleOnLevelCompletedEvent()
    {
        _levelContext.Player.LockInput();
        
       //Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
       //          "<color=yellow> REWARD OVERALL - [" +
       //          _rewardCoordinator.GetAllAssignedRewardValue() + "] <color=yellow>");
        
        //yield return new WaitForSeconds(_delayLevelCompletedEvent);
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=red> ALL ENEMIES DIED <color=red>");
            
        foreach (var shootedBullet in _levelContext.ShootedBullets)
            if (shootedBullet != null)
                shootedBullet.FreezeMove();
    }
    
    private void HandleAllEnemyDiedEvent()
    {
        //OnAllRewardPointsAssigned?.Invoke(_rewardCoordinator.GetAllAssignedRewardValue());
        
        //Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
        //          "<color=yellow> REWARD OVERALL - [" +
        //          _rewardCoordinator.GetAllAssignedRewardValue() + "] <color=yellow>");
        
        //yield return new WaitForSeconds(_delayDispatch);
        
        //OnAllEnemiesDied?.Invoke();

        //Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
        //          "<color=red> ALL ENEMIES DIED <color=red>");
        
        //DispatchShootedBulletsFreeze();
    }
}