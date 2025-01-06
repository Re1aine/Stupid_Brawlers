using System;
using UnityEngine;

public class GunView : MonoBehaviour
{
    public event Action OnShoot; 
    public event Action OnAllBulletsFinished;

    public ReadOnlyReactiveProperty<int> BulletCount => new(_bulletCount);
    private readonly ReactiveProperty<int> _bulletCount = new();
    
    [SerializeField] private Transform _shootPoint;
    [SerializeField, Range(0f, 1.5f)] private float _delayRecharge;

    private LevelFactory _levelFactory;
    
    private Vector3 _shootDirection;
    private float _timeRemainToShoot;

    public void Construct(LevelFactory levelFactory)
    {
        _levelFactory = levelFactory;
    }
    
    public void SetBulletCount(int value)
    {
        _bulletCount.SetValue(value);
    }
    
    public void Update() => _timeRemainToShoot -= Time.deltaTime;
    
    public void Shoot()
    {
        if (_bulletCount.Value <= 0)
        {
            OnAllBulletsFinished?.Invoke();
            return;
        }
        
        if(_timeRemainToShoot > 0) return;      
            
        _levelFactory.CreateBullet(_shootPoint.position, _shootDirection);
        
        SetBulletCount(_bulletCount.Value - 1);
        
        _timeRemainToShoot = _delayRecharge;
        
        OnShoot?.Invoke();
    }

    public Vector3 GetShootPoint() => _shootPoint.position;

    public void SetShootDirection(Vector3 direction) => 
        _shootDirection = direction;
}