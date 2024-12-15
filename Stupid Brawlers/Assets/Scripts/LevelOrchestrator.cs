using System;
using Object = UnityEngine.Object;

public class LevelOrchestrator : IDisposable
{
    private readonly GameFactory _gameFactory;
    private readonly ICoroutineExecutor _coroutineExecutor;

    private LevelFactory _levelFactory;
    private LevelContext _context;
    private LevelDispatcher _dispatcher;
    private RewardCoordinator _rewardCoordinator;
    private SceneContainer _sceneContainer;
    
    private LevelEventBinder _levelEventBinder;
    private LevelEventHandler _levelEventHandler;

    public LevelOrchestrator(GameFactory gameFactory, ICoroutineExecutor coroutineExecutor)
    {
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
        _dispatcher = new LevelDispatcher(_context, _rewardCoordinator,_coroutineExecutor);
        _levelFactory = new LevelFactory(_context, _dispatcher);
        
        var popupMaster = _levelFactory.CreatePopupMaster();

        _levelEventBinder = new LevelEventBinder(_context, _dispatcher);
        _levelEventHandler = new LevelEventHandler(_context, _rewardCoordinator, popupMaster, _coroutineExecutor, _dispatcher);

        var player = _levelFactory.CreatePlayer(_sceneContainer.PlayerSpawnPoint.transform.position,3);
        _levelEventBinder.BindPlayer(player);
        
        _levelEventHandler.Subscribe();
        
        //_dispatcher.OnEnemyDied += levelEventHandler.HandleOnEnemyDiedEvent;
        //_dispatcher.OnLevelCompleted += levelEventHandler.HandleDelayedOnLevelCompletedEvent;

        
        foreach (var point in _sceneContainer.EnemySpawnPoints)
        {
            var enemy = _levelFactory.CreateEnemy(point.transform.position);
            _levelEventBinder.BindEnemy(enemy);
        }

        var uiContainer = _gameFactory.CreateUIContainer();
        uiContainer.Construct(_context, _dispatcher);
        uiContainer.Run();
    }

    public void Dispose()
    {
        //_context.Dispose();
        //_levelFactory.Dispose();
        //_levelDispatcher.Dispose();
        //_rewardCoordinator.Dispose();
        
        _levelEventHandler.UnSubscribe();
    }
}