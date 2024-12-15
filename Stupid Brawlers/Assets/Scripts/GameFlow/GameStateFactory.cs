
public class GameStateFactory : IGameStateFactory
{
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly CoroutineExecutor _coroutineExecutor;
    private GameStateMachine _gameStateMachine;
    
    public GameStateFactory(GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster, CoroutineExecutor coroutineExecutor)
    {
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
        _coroutineExecutor = coroutineExecutor;
    }

    public void SetGameStateMachine(GameStateMachine gameStateMachine) => 
        _gameStateMachine = gameStateMachine;

    public LoadMenuState CreateLoadMenuState() => 
        new(_gameStateMachine, _gameFactory, _loadSceneMaster);

    public InitState CreateInitState() => 
        new(_gameStateMachine, _loadSceneMaster, _gameFactory);

    public LoadLevelState CreateLoadLevelState() => 
        new(_gameStateMachine, _gameFactory, _loadSceneMaster, _coroutineExecutor);
}