using UnityEngine;

public class LevelFactory
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly LevelContext _levelContext;

    public LevelFactory(GameStateMachine gameStateMachine, LevelContext levelContext)
    {
        _gameStateMachine = gameStateMachine;
        _levelContext = levelContext;
    }
    
    public PlayerView CreatePlayer(Vector3 position, int bulletCount)
    {
        var playerView = AssetProvider.InstantiateAt<PlayerView>(AssetDataPath.PlayerPrefab, position);
        Player player = new Player(playerView);

        _levelContext.Player = playerView;
        
        playerView.Construct(player);
        
        playerView.GunView.Construct(this);
        playerView.GunView.SetBulletCount(bulletCount);
        
        return playerView;
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        var enemy =  AssetProvider.InstantiateAt<Enemy>(AssetDataPath.EnemyPrefab, position);
        _levelContext.AddEnemy(enemy);
        return enemy;
    }

    public Bullet CreateBullet(Vector3 position, Vector3 direction)
    {
        var bullet = AssetProvider.InstantiateAt<Bullet>(AssetDataPath.BulletPrefab, position);
        bullet.SetMoveDirection(direction);
        bullet.RotateToDirection(direction);
        
        _levelContext.AddShootedBullet(bullet);
        
        return bullet;
    }
    
    public UIContainer CreateUIContainer()
    {
        var ui =  AssetProvider.Instantiate<UIContainer>(AssetDataPath.UIContainerPrefab);
        ui.SetGameStateMachine(_gameStateMachine);
        
        _levelContext.SetUI(ui);

        return ui;
    }
    
    public PopupMaster CreatePopupMaster() => 
        AssetProvider.Instantiate<PopupMaster>(AssetDataPath.PopupMaster);
}