
public class LoadLevelState : IParameterizedState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly CoroutineExecutor _coroutineExecutor;
    private readonly GlobalUIContainer _globalUI;
    private readonly AudioService _audioService;

    private LoadScreen _loadScreen;
    
    private LevelOrchestrator _levelOrchestrator;

    public LoadLevelState(GameStateMachine gameStateMachine,
        GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster, CoroutineExecutor coroutineExecutor, GlobalUIContainer globalUI, AudioService audioService)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
        _coroutineExecutor = coroutineExecutor;
        _globalUI = globalUI;
        _audioService = audioService;
    }
    
    public void Enter(string sceneName)
    {
        _globalUI.ShowLoadScreen();
        _loadSceneMaster.LoadSceneByName(sceneName, OnLoad);
    }

    private void OnLoad()
    {
        _levelOrchestrator = new LevelOrchestrator(_gameStateMachine, _coroutineExecutor, _globalUI, _audioService);
        _levelOrchestrator.Run();
            
        _globalUI.HideLoadScreen();
    }

    public void Exit()
    {
        _levelOrchestrator.Dispose();
    }
}