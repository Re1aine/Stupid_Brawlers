using UnityEngine;
using UnityEngine.UI;

public class BulletField : MonoBehaviour
{
    [SerializeField] private Image _bulletIcon;
    [SerializeField] private Transform _content;
    
    public void AddBullet(int value)
    {
        for (int i = 0; i < value; i++) 
            Instantiate(_bulletIcon, _content);
    }
    
    public void SubtractBullet()
    {
        if(_content.childCount == 0) 
            return;
        
        Destroy(_content.GetChild(0).gameObject);
    }
}