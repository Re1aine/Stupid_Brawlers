public class LevelInformer
{
    private readonly LevelId _id;
    private int _highScore;

    public LevelInformer() => _id = GetLvlId();
    public void SetHighScore(int value) => _highScore = value;
    public int GetHighScore() => _highScore;
    public string GetLvlKey() => _id.ToString();
    private LevelId GetLvlId() => SceneNavigator.GetCurrentLevelId();
}