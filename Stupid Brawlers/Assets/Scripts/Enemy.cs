using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int RewardValue = 500;
    
    public event Action<Enemy> OnDied;

    public void Die()
    {
        Destroy(gameObject);
        OnDied?.Invoke(this);
    }

    public int GetRewardValue() => RewardValue;
}