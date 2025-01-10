using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private GameFactory _gameFactory;
    private GameStateFactory _gameStateFactory;
    private GameStateMachine _gameStateMachine;
    private CoroutineExecutor _coroutineExecutor;
    private LoadSceneMaster _loadSceneMaster;
    private GlobalUIContainer _globalUI;
    private AudioService _audioService;
    
    private void Awake()
    {
        _coroutineExecutor = GetComponentInChildren<CoroutineExecutor>();
        
        _globalUI = GetComponentInChildren<GlobalUIContainer>();
        _globalUI.Construct(_coroutineExecutor);
        
        _gameFactory = new GameFactory(transform);
        
        _loadSceneMaster = new LoadSceneMaster(_coroutineExecutor);
        
        _audioService = new AudioService(_gameFactory);

        _gameStateFactory = new GameStateFactory(_gameFactory, _loadSceneMaster, _coroutineExecutor, _globalUI, _audioService);
        
        _gameStateMachine = new GameStateMachine(_gameStateFactory);
        
        _gameFactory.SetGameStateMachine(_gameStateMachine);
        
        _gameStateMachine.Run();
        
        DontDestroyOnLoad(this);
    }
}