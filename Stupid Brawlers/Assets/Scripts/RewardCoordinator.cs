using UnityEngine;

public class RewardCoordinator
{
    //public event Action OnRewardAssigned
    
    private readonly PointsCounter _pointsCounter = new();

    public void AssignRewardForEnemy(Enemy enemy)
    {
        _pointsCounter.AddPoints(enemy.GetRewardValue());
        
        Debug.Log("<b><color=green> [LEVEL DISPATCHER] <color=green>" +
                  "<color=yellow> REWARD ASSIGNED FOR ENEMY - [" + 
                  enemy.GetRewardValue() + "] <color=yellow>");
    }
    
    //public int GetAllAssignedRewardValue()
    //{
    //    return _pointsCounter.GetReceivedPoints();
    //}
}