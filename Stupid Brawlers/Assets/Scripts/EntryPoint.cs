using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private GameFactory _gameFactory;
    private GameStateFactory _gameStateFactory;
    private GameStateMachine _gameStateMachine;
    private CoroutineExecutor _coroutineExecutor;
    private LoadSceneMaster _loadSceneMaster;

    private void Awake()
    {
        _coroutineExecutor = GetComponentInChildren<CoroutineExecutor>();
        
        _gameFactory = new GameFactory();

        _loadSceneMaster = new LoadSceneMaster(_coroutineExecutor);
        
        _gameStateFactory = new GameStateFactory(_gameFactory, _loadSceneMaster, _coroutineExecutor);
        
        _gameStateMachine = new GameStateMachine(_gameStateFactory);
        
        _gameFactory.SetGameStateMachine(_gameStateMachine);
        
        _gameStateMachine.Run();
        
        DontDestroyOnLoad(this);
    }
}