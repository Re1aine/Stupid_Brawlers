using UnityEngine;
using UnityEngine.UI;

public class BulletField : MonoBehaviour
{
    [SerializeField] private Image _bulletIcon;
    [SerializeField] private Transform _content;

    private int _currentBulletsCount;

    public void UpdateBulletCount(int value)
    {
        if (value > _currentBulletsCount)
            AddBullet(value - _currentBulletsCount);
        else if (value < _currentBulletsCount)
            SubtractBullet();

        _currentBulletsCount = value;
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
}