using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform PositionPopUp => _positionPopUp;
    
    [SerializeField] private int _rewardValue;
    [SerializeField] private Transform _positionPopUp;
    
    public event Action<Enemy> OnDied;

    public void Die()
    {
        Destroy(gameObject);
        OnDied?.Invoke(this);
    }

    public int GetRewardValue() => _rewardValue;
}