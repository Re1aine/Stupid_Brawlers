
public class GameFactory
{
    private GameStateMachine _gameStateMachine;

    public void SetGameStateMachine(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public UIMenu CreateUIMenu() => 
        AssetProvider.Instantiate<UIMenu>(AssetDataPath.UIMenuPrefab);
}