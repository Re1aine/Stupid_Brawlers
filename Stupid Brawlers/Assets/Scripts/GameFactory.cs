public class GameFactory
{
    private GameStateMachine _gameStateMachine;

    public void SetGameStateMachine(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public UIMenu CreateUIMenu() => 
        AssetProvider.Instantiate<UIMenu>(AssetDataPath.UIMenuPrefab);

    public UIContainer CreateUIContainer()
    {
        var ui =  AssetProvider.Instantiate<UIContainer>(AssetDataPath.UIContainerPrefab);
        ui.SetGameStateMachine(_gameStateMachine);

        return ui;
    }
}