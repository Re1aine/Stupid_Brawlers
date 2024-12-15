
public class LoadMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;

    public LoadMenuState(GameStateMachine gameStateMachine,
        GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
    }

    public void Enter()
    {
        var sceneMenuName = SceneNavigator.GetName(GameScene.Menu);
        _loadSceneMaster.LoadSceneByName(sceneMenuName, OnLoad);
    }

    private void OnLoad()
    {
        var uiMenu = _gameFactory.CreateUIMenu();
        uiMenu.Construct(_gameStateMachine);
    }
    
    public void Exit()
    {
        
    }
}