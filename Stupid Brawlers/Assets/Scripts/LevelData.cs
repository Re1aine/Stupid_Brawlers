using System;

[Serializable]
public class LevelData
{
    public LevelState _state;
    public int _highScore;

    public void SetLevelState(LevelState state) => _state = state;
    public void SetHighScore(int value) => _highScore = value;
    public LevelState GetLevelState() => _state;
    public int GetHighScore() => _highScore;
}