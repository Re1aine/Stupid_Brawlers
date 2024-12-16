using System;
using Object = UnityEngine.Object;

public class LevelOrchestrator : IDisposable
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly ICoroutineExecutor _coroutineExecutor;

    private LevelContext _context;
    private LevelFactory _levelFactory;
    private LevelDispatcher _dispatcher;
    private RewardCoordinator _rewardCoordinator;
    private LevelEntityEventMatcher _levelEntityEventMatcher;
    private LevelEventHandler _levelEventHandler;
    private SceneGC _sceneGC;
    
    private SceneContainer _sceneContainer;

    public LevelOrchestrator(GameStateMachine gameStateMachine, ICoroutineExecutor coroutineExecutor)
    {
        _gameStateMachine = gameStateMachine;
        _coroutineExecutor = coroutineExecutor;
    }

    private void InitSceneContainer()
    {
       _sceneContainer = Object.FindAnyObjectByType<SceneContainer>();
       if (_sceneContainer == null)
           throw new InvalidOperationException("SceneContainer not found in the scene.");
    }

    public void Run()
    {
        InitSceneContainer();
        
        
        _sceneGC = new SceneGC();
        _context = new LevelContext();
        _dispatcher = new LevelDispatcher();
        _rewardCoordinator = new RewardCoordinator();
        _levelFactory = new LevelFactory(_gameStateMachine, _context, _sceneGC);
        _levelEntityEventMatcher = new LevelEntityEventMatcher(_context, _dispatcher, _rewardCoordinator);
        _levelEventHandler = new LevelEventHandler(_context, _rewardCoordinator, _coroutineExecutor, _dispatcher);

        
        _sceneContainer.EnemySpawnPoints.ForEach(p => _levelFactory.CreateEnemy(p.transform.position));
        _levelFactory.CreatePlayer(_sceneContainer.PlayerSpawnPoint.transform.position,3);
        _levelFactory.CreateUIContainer();
        _levelFactory.CreatePopupMaster();

        
        _levelEntityEventMatcher.Run();
        _levelEventHandler.Run();
    }

    public void Dispose()
    {
        _rewardCoordinator.Dispose();
        _levelEntityEventMatcher.Dispose();
        _levelEventHandler.Dispose();
        _sceneGC.Dispose();
        _context.Dispose();
    }
}