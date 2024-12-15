using System;
using System.Linq;
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
        _dispatcher = new LevelDispatcher();
        _levelFactory = new LevelFactory(_context);
        _levelEventBinder = new LevelEventBinder(_context, _dispatcher, _rewardCoordinator);
        
        var popupMaster = _levelFactory.CreatePopupMaster();
        
        _levelEventHandler = new LevelEventHandler(_context, _rewardCoordinator, popupMaster, _coroutineExecutor, _dispatcher);
        _levelEventHandler.Run();


        var player = _levelFactory.CreatePlayer(_sceneContainer.PlayerSpawnPoint.transform.position,3);

        foreach (var point in _sceneContainer.EnemySpawnPoints) 
            _levelFactory.CreateEnemy(point.transform.position);

        var uiContainer = _gameFactory.CreateUIContainer();
        uiContainer.Construct(_context, _dispatcher, _rewardCoordinator);
        uiContainer.Run();


        foreach (var enemy in _context.Enemies)
            _levelEventBinder.BindEnemy(enemy);
        _levelEventBinder.BindPlayer(player);
        _levelEventBinder.BindUI(uiContainer);
    }

    public void Dispose()
    {
        _rewardCoordinator.Dispose();
        _levelEventBinder.Dispose();
        _levelEventHandler.Dispose();
        _context.Dispose();
    }
}