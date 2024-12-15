using System;
using UnityEngine;

public class LevelFactory : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly LevelDispatcher _levelDispatcher;

    public LevelFactory(LevelContext levelLevelContext, LevelDispatcher levelDispatcher)
    {
        _levelContext = levelLevelContext;
        _levelDispatcher = levelDispatcher;
    }
    
    public PlayerView CreatePlayer(Vector3 position, int bulletCount)
    {
        var playerView = AssetProvider.InstantiateAt<PlayerView>(AssetDataPath.PlayerPrefab, position);
        Player player = new Player(playerView);

        playerView.Construct(player);
        playerView.GunView.Construct(this, bulletCount);

        _levelContext.Player = playerView;
        
        //_levelContext.Player.GunView.OnShoot += _levelDispatcher.DispatchPlayerWantsToShoot;
        //_levelContext.Player.GunView.OnAllBulletsFinished += _levelDispatcher.DispatchAllBulletsFinished;
        //_levelDispatcher.OnPlayerLockInput += _levelContext.Player.Lock;
        
        return playerView;
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        var enemy =  AssetProvider.InstantiateAt<Enemy>(AssetDataPath.EnemyPrefab, position);
        _levelContext.AddEnemy(enemy);

        //enemy.OnDied += _levelDispatcher.DispatchEnemyDied;

        return enemy;
    }

    public Bullet CreateBullet(Vector3 position, Vector3 direction)
    {
        var bullet = AssetProvider.InstantiateAt<Bullet>(AssetDataPath.BulletPrefab, position);
        bullet.SetMoveDirection(direction);
        bullet.RotateToDirection(direction);
        
        bullet.Construct(_levelDispatcher);
        
        _levelContext.AddShootedBullet(bullet);
        
        //bullet.OnDestroyed += _levelContext.RemoveShootedBullet;
        //_levelDispatcher.OnFreezeBullet += bullet.FreezeMove;
        
        
        return bullet;
    }

    public PopupMaster CreatePopupMaster()
    {
        var popupMaster =  AssetProvider.Instantiate<PopupMaster>(AssetDataPath.PopupMaster);
        //_levelDispatcher.OnCreatePopup += popupMaster.CreatePopupAt;

        return popupMaster;
    }

    public void Dispose()
    {
        //foreach (var enemy in _levelContext.Enemies) 
        //    enemy.OnDied -= _levelDispatcher.DispatchEnemyDied;
        //
        //_levelContext
        //    .Player
        //    .GunView.OnShoot -= _levelDispatcher.DispatchPlayerWantsToShoot;
        //
        //_levelContext
        //    .Player
        //    .GunView.OnAllBulletsFinished -= _levelDispatcher.DispatchAllBulletsFinished;
        //
        //_levelDispatcher.OnPlayerLockInput -= _levelContext.Player.LockInput;
    }
}

public class LevelEventBinder : IDisposable
{
    private readonly LevelContext _levelContext;
    private readonly LevelDispatcher _levelDispatcher;

    public LevelEventBinder(LevelContext levelContext, LevelDispatcher levelDispatcher)
    {
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
    }    
    
    public void BindPlayer(PlayerView playerView)
    {
        playerView.GunView.OnShoot += _levelDispatcher.DispatchPlayerWantsToShoot;
        playerView.GunView.OnAllBulletsFinished += _levelDispatcher.DispatchAllBulletsFinished;
        
        //_levelDispatcher.OnPlayerLockInput += playerView.LockInput;
    }

    public void BindEnemy(Enemy enemy)
    {
        enemy.OnDied += _levelDispatcher.DispatchEnemyDied;
    }

    private void BindBullet(Bullet bullet)
    {
        bullet.OnDestroyed += _levelContext.RemoveShootedBullet;
        
        //_levelDispatcher.OnFreezeBullet += bullet.FreezeMove;
    }

    private void BindPopupMaster(PopupMaster popupMaster)
    {
        //_levelDispatcher.OnCreatePopup += popupMaster.CreatePopUpAt;
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