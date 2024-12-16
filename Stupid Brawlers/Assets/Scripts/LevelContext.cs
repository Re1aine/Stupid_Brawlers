using System;
using System.Collections.Generic;
using System.Linq;

public class LevelContext : IDisposable
{
    public PlayerView Player { get; set; }
    public List<Enemy> Enemies { get; } = new();
    public List<Bullet> ShootedBullets { get; set; } = new();
    
    public UIContainer UI { get; set; }
    
    public void RemoveEnemy(Enemy enemy) => Enemies.Remove(enemy);
    public void AddEnemy(Enemy enemy) => Enemies.Add(enemy);
    public void AddShootedBullet(Bullet bullet) => ShootedBullets.Add(bullet);
    public void RemoveShootedBullet(Bullet bullet) => ShootedBullets.Remove(bullet);
    private void SetPlayer(PlayerView player) => Player = player;
    public void SetUI(UIContainer ui) => UI = ui;
    

    public void Dispose()
    {
        SetPlayer(null);
        SetUI(null);

        foreach (var enemy in Enemies.Where(enemy => enemy != null).ToList()) 
            RemoveEnemy(enemy);

        foreach (var bullet in ShootedBullets.Where(bullet => bullet != null).ToList()) 
            RemoveShootedBullet(bullet);
    }
}