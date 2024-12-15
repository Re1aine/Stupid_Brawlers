using System;
using UnityEngine;

public class GunView : MonoBehaviour
{
    public event Action OnShoot; 
    public event Action OnAllBulletsFinished;
    
    [SerializeField] private Transform _shootPoint;
    [SerializeField, Range(0f, 1.5f)] private float _delayRecharge;

    private LevelFactory _levelFactory;

    private Vector3 _shootDirection;
    private int _bulletCount;
    
    private float _timeRemainToShoot;
    
    public void Construct(LevelFactory levelFactory, int bulletCount)
    {
        _levelFactory = levelFactory;
        _bulletCount = bulletCount;
    }
    
    public void SetBulletCount(int bulletCount) => _bulletCount = bulletCount;
    
    public int GetBulletCount() => _bulletCount;

    public void Update() => _timeRemainToShoot -= Time.deltaTime;

    public void Shoot()
    {
        if (_bulletCount <= 0)
        {
            OnAllBulletsFinished?.Invoke();
            return;
        }
        
        if(_timeRemainToShoot > 0) return;      
            
        _levelFactory.CreateBullet(_shootPoint.position, _shootDirection);
        
        _bulletCount -= 1;
        _timeRemainToShoot = _delayRecharge;
        
        OnShoot?.Invoke();
    }

    public Vector3 GetShootPoint() => _shootPoint.position;

    public void SetShootDirection(Vector3 direction) => 
        _shootDirection = direction;
}