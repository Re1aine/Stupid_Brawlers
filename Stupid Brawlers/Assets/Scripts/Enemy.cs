using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _rewardValue;
    
    public event Action<Enemy> OnDied;

    public void Die()
    {
        Destroy(gameObject);
        OnDied?.Invoke(this);
    }

    public int GetRewardValue() => _rewardValue;
}