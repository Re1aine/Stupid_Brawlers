using System;
using System.Collections.Generic;
using System.Linq;

public class LevelContext : IDisposable
{
    public PlayerView Player { get; set; }
    public List<Enemy> Enemies { get; } = new();
    public List<Bullet> ShootedBullets { get; set; } = new();

    public void RemoveEnemy(Enemy enemy) => Enemies.Remove(enemy);
    public void AddEnemy(Enemy enemy) => Enemies.Add(enemy);
    public void AddShootedBullet(Bullet bullet) => ShootedBullets.Add(bullet);
    public void RemoveShootedBullet(Bullet bullet) => ShootedBullets.Remove(bullet);
    private void RemovePlayer() => Player = null;

    public void Dispose()
    {
        RemovePlayer();

        foreach (var enemy in Enemies.Where(enemy => enemy != null)) 
            RemoveEnemy(enemy);

        foreach (var bullet in ShootedBullets.Where(bullet => bullet != null).ToList()) 
            RemoveShootedBullet(bullet);
    }
}