
public class GameStateFactory : IGameStateFactory
{
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly CoroutineExecutor _coroutineExecutor;
    private readonly GlobalUIContainer _globalUI;
    private GameStateMachine _gameStateMachine;
    private readonly AudioService _audioService;

    public GameStateFactory(GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster, CoroutineExecutor coroutineExecutor, GlobalUIContainer globalUI,
        AudioService audioService)
    {
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
        _coroutineExecutor = coroutineExecutor;
        _globalUI = globalUI;
        _audioService = audioService;
    }

    public void SetGameStateMachine(GameStateMachine gameStateMachine) => 
        _gameStateMachine = gameStateMachine;

    public LoadMenuState CreateLoadMenuState() => 
        new(_gameStateMachine, _gameFactory, _loadSceneMaster, _globalUI, _audioService);

    public InitState CreateInitState() => 
        new(_gameStateMachine, _loadSceneMaster, _gameFactory);

    public LoadLevelState CreateLoadLevelState() => 
        new(_gameStateMachine, _gameFactory, _loadSceneMaster, _coroutineExecutor, _globalUI, _audioService);
}