using UnityEngine;

public class LevelFactory
{
    private readonly LevelContext _levelContext;

    public LevelFactory(LevelContext levelLevelContext)
    {
        _levelContext = levelLevelContext;
    }
    
    public PlayerView CreatePlayer(Vector3 position, int bulletCount)
    {
        var playerView = AssetProvider.InstantiateAt<PlayerView>(AssetDataPath.PlayerPrefab, position);
        Player player = new Player(playerView);

        playerView.Construct(player);
        playerView.GunView.Construct(this, bulletCount);

        _levelContext.Player = playerView;
        
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

    public PopupMaster CreatePopupMaster() => 
        AssetProvider.Instantiate<PopupMaster>(AssetDataPath.PopupMaster);
}