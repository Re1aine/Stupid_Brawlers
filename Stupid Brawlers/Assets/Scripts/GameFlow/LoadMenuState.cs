
public class LoadMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly GlobalUIContainer _globalUI;

    private bool _fromBoostrapState = true;
    
    public LoadMenuState(GameStateMachine gameStateMachine,
        GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster,
        GlobalUIContainer globalUI)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
        _globalUI = globalUI;
    }

    public void Enter()
    {
        if (SceneNavigator.GetCurrentGameScene() != GameScene.Bootstrap)
        {
            _fromBoostrapState = false;
            _globalUI.ShowLoadScreen();
        }
        
        var sceneMenuName = SceneNavigator.GetName(GameScene.Menu);
        _loadSceneMaster.LoadSceneByName(sceneMenuName, OnLoad);
    }

    private void OnLoad()
    {
        if(!_fromBoostrapState) _globalUI.HideLoadScreen();
            
        var uiMenu = _gameFactory.CreateUIMenu();
        uiMenu.Construct(_gameStateMachine);
    }
    
    public void Exit()
    {
        
    }
}