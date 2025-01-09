
public class LevelInformer
{
    public LevelData LevelData => _levelData;
    
    private LevelData _levelData;
    
    private string _levelKey;
    
    public void SetLevelData(LevelData levelData) => _levelData = levelData;
   
    public string GetLevelKey() => SceneNavigator.GetCurrentLvlName();

}

public enum LevelState
{
    UnCompleted = 0,
    Completed = 1,
}