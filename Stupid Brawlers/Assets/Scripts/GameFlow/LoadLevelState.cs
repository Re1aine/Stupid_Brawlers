using System;

public class LoadLevelState : IParameterizedState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly GameFactory _gameFactory;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly CoroutineExecutor _coroutineExecutor;

    private LevelOrchestrator _levelOrchestrator;

    public LoadLevelState(GameStateMachine gameStateMachine,
        GameFactory gameFactory,
        LoadSceneMaster loadSceneMaster, CoroutineExecutor coroutineExecutor)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _loadSceneMaster = loadSceneMaster;
        _coroutineExecutor = coroutineExecutor;
    }
    
    public void Enter(string sceneName)
    {
        _loadSceneMaster.LoadSceneByName(sceneName, OnLoad);
    }

    private void OnLoad()
    {
         _levelOrchestrator = new LevelOrchestrator(_gameStateMachine, _gameFactory, _coroutineExecutor);
         _levelOrchestrator.Run();
    }

    public void Exit()
    {
        _levelOrchestrator.Dispose();
    }
}

public enum GameScene
{
    Bootstrap = 0,
    Menu = 1,
}