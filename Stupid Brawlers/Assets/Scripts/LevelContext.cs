using System.Collections.Generic;

public class LevelContext
{
    public PlayerView Player { get; set; }
    public List<Enemy> Enemies { get; } = new();
    public List<Bullet> ShootedBullets { get; set; } = new();
    
    public void RemoveEnemy(Enemy enemy) => Enemies.Remove(enemy);
    public void AddEnemy(Enemy enemy) => Enemies.Add(enemy);
    public void AddShootedBullet(Bullet bullet) => ShootedBullets.Add(bullet);
    public void RemoveShootedBullet(Bullet bullet) => ShootedBullets.Remove(bullet);
}