using System;
using UnityEngine;
using UnityEngine.UI;

public class BulletField : MonoBehaviour, IDisposable
{
    [SerializeField] private Image _bulletIcon;
    [SerializeField] private Transform _content;

    private ReadOnlyReactiveProperty<int> _bulletCount;
    
    private int _currentBulletsCount;

    public void UpdateBulletCount(int value)
    {
        if (value > _currentBulletsCount)
            AddBullet(value - _currentBulletsCount);
        else if (value < _currentBulletsCount)
            SubtractBullet();

        _currentBulletsCount = value;
    }

    public void InitBulletCount(ReadOnlyReactiveProperty<int> value)
    {
        Dispose();
        _bulletCount = value;
        _bulletCount.OnValueChanged += UpdateBulletCount;
        UpdateBulletCount(_bulletCount.Value);
    }
    
    private void AddBullet(int value)
    {
        for (int i = 0; i < value; i++) 
            Instantiate(_bulletIcon, _content);
    }

    private void SubtractBullet()
    {
        if(_content.childCount == 0) 
            return;
        
        Destroy(_content.GetChild(0).gameObject);
    }

    public void Dispose()
    {
        if(_bulletCount != null)
            _bulletCount.OnValueChanged -= UpdateBulletCount;
    }
}