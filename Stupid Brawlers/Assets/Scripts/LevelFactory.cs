using UnityEngine;

public class LevelFactory
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly LevelContext _levelContext;
    private readonly SceneGC _sceneGC;
    private readonly AudioService _audioService;

    public LevelFactory(GameStateMachine gameStateMachine, LevelContext levelContext, SceneGC sceneGC, AudioService audioService)
    {
        _gameStateMachine = gameStateMachine;
        _levelContext = levelContext;
        _sceneGC = sceneGC;
        _audioService = audioService;
    }
    
    public PlayerView CreatePlayer(Vector3 position, int bulletCount)
    {
        var playerView = AssetProvider.InstantiateAt<PlayerView>(AssetDataPath.PlayerPrefab, position);
        Player player = new Player(playerView);

        _levelContext.SetPlayer(playerView);
        _sceneGC.AddEntity(playerView);
        
        playerView.Construct(player);
        
        playerView.GunView.Construct(this, _audioService);
        playerView.GunView.SetBulletCount(bulletCount);
        
        return playerView;
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        var enemy =  AssetProvider.InstantiateAt<Enemy>(AssetDataPath.EnemyPrefab, position);
        
        _levelContext.AddEnemy(enemy);
        _sceneGC.AddEntity(enemy);
        
        return enemy;
    }

    public Bullet CreateBullet(Vector3 position, Vector3 direction)
    {
        var bullet = AssetProvider.InstantiateAt<Bullet>(AssetDataPath.BulletPrefab, position);
        bullet.SetMoveDirection(direction);
        bullet.RotateToDirection(direction);

        bullet.Construct(_audioService);
        
        _levelContext.AddShootedBullet(bullet);
        _sceneGC.AddEntity(bullet);
        
        return bullet;
    }
    
    public UIContainer CreateUIContainer()
    {
        var ui =  AssetProvider.Instantiate<UIContainer>(AssetDataPath.UIContainerPrefab);
        ui.SetGameStateMachine(_gameStateMachine);
        ui.Run();

        _levelContext.SetUI(ui);
        _sceneGC.AddEntity(ui);
        
        return ui;
    }
    
    public PopupMaster CreatePopupMaster()
    {
        var popUpMaster = AssetProvider.Instantiate<PopupMaster>(AssetDataPath.PopupMaster);
        
        _levelContext.SetPopupMaster(popUpMaster);
        _sceneGC.AddEntity(popUpMaster);
        
        return popUpMaster;
    }
}