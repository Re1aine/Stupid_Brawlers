using System.Collections.Generic;
using UnityEngine;

public class LevelInformer
{
    public LevelData LevelData => _levelData;
    
    private LevelData _levelData;
    
    private readonly LevelId _id;

    public LevelInformer() => _id = GetLvlId();
    
    public void SetLevelData(LevelData levelData) => _levelData = levelData;
    public string GetLvlKeyId() => _id.ToString();
    private LevelId GetLvlId() => SceneNavigator.GetCurrentLevelId();
}

public enum LevelState
{
    UnCompleted = 0,
    Completed = 1,
}