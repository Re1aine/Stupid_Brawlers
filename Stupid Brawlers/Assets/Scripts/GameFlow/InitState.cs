public class InitState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly LoadSceneMaster _loadSceneMaster;
    private readonly GameFactory _gameFactory;

    public InitState(GameStateMachine gameStateMachine, LoadSceneMaster loadSceneMaster, GameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _loadSceneMaster = loadSceneMaster;
        _gameFactory = gameFactory;
    }
    
    public void Enter()
    {
        _gameStateMachine.Enter<LoadMenuState>();
    }

    public void Exit()
    {
        
    }
}