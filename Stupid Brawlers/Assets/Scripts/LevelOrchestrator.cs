using System;
using System.Linq;
using Object = UnityEngine.Object;

public class LevelOrchestrator : IDisposable
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly GameFactory _gameFactory;
    private readonly ICoroutineExecutor _coroutineExecutor;

    private LevelFactory _levelFactory;
    private LevelContext _context;
    private LevelDispatcher _dispatcher;
    private RewardCoordinator _rewardCoordinator;
    private SceneContainer _sceneContainer;
    
    private LevelEntityEventMatcher _levelEntityEventMatcher;
    private LevelEventHandler _levelEventHandler;

    public LevelOrchestrator(GameStateMachine gameStateMachine, GameFactory gameFactory, ICoroutineExecutor coroutineExecutor)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _coroutineExecutor = coroutineExecutor;
    }

    private void InitSceneContainer() => 
        _sceneContainer = Object.FindAnyObjectByType<SceneContainer>();

    public void Run()
    {
        InitSceneContainer();
        
        _sceneContainer.Run();

        _rewardCoordinator = new RewardCoordinator();
        _context = new LevelContext();
        _dispatcher = new LevelDispatcher();
        _levelFactory = new LevelFactory(_gameStateMachine, _context);
        _levelEntityEventMatcher = new LevelEntityEventMatcher(_context, _dispatcher, _rewardCoordinator);
        
        var popupMaster = _levelFactory.CreatePopupMaster();
        
        _levelEventHandler = new LevelEventHandler(_context, _rewardCoordinator, popupMaster, _coroutineExecutor, _dispatcher);
        _levelEventHandler.Run();


        var uiContainer = _levelFactory.CreateUIContainer();
        uiContainer.Run();
        
        foreach (var point in _sceneContainer.EnemySpawnPoints) 
            _levelFactory.CreateEnemy(point.transform.position);

        var player = _levelFactory.CreatePlayer(_sceneContainer.PlayerSpawnPoint.transform.position,3);
        
        
        foreach (var enemy in _context.Enemies)
            _levelEntityEventMatcher.BindEnemy(enemy);
        _levelEntityEventMatcher.BindPlayer(player, uiContainer);
        _levelEntityEventMatcher.BindUI(uiContainer);
    }

    public void Dispose()
    {
        _rewardCoordinator.Dispose();
        _levelEntityEventMatcher.Dispose();
        _levelEventHandler.Dispose();
        _context.Dispose();
    }
}