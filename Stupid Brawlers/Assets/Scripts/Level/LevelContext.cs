using System;
using System.Collections.Generic;
using System.Linq;

public class LevelContext : IDisposable
{
    public PlayerView Player { get; private set; }
    public List<Enemy> Enemies { get; } = new();
    public List<Bullet> ShootedBullets { get; } = new();
    public UIContainer UI { get; private set; }
    public PopupMaster PopupMaster { get; private set; }
    
    public void RemoveEnemy(Enemy enemy) => Enemies.Remove(enemy);
    public void AddEnemy(Enemy enemy) => Enemies.Add(enemy);
    public void AddShootedBullet(Bullet bullet) => ShootedBullets.Add(bullet);
    public void SetUI(UIContainer ui) => UI = ui;
    private void RemoveShootedBullet(Bullet bullet) => ShootedBullets.Remove(bullet);
    public void SetPlayer(PlayerView player) => Player = player;
    public void SetPopupMaster(PopupMaster popupMaster) => PopupMaster = popupMaster;
    
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