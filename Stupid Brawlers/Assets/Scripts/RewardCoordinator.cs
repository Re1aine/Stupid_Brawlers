using System;
using UnityEngine;

public class RewardCoordinator : IDisposable
{
    public event Action<int> OnRewardAssigned;
    
    private readonly PointsCounter _pointsCounter = new();

    public RewardCoordinator()
    {
        _pointsCounter.ReceivePoints.OnValueChanged += RewardAssigned;
    }

    private void RewardAssigned(int value) => OnRewardAssigned?.Invoke(value);

    public void AssignRewardForEnemy(Enemy enemy)
    {
        _pointsCounter.AddPoints(enemy.GetRewardValue());
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=yellow> REWARD ASSIGNED FOR ENEMY - [" + 
                  enemy.GetRewardValue() + "] <color=yellow>");
    }
    
    public void Dispose() => _pointsCounter.ReceivePoints.OnValueChanged -= RewardAssigned;
}