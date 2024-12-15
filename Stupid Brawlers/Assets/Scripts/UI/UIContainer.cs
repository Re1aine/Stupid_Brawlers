using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    public BulletField BulletField => _bulletField;
    public LvlCompleteWindow LvlCompleteWindow => _completeWindow;
    
    [SerializeField] private BulletField _bulletField;
    [SerializeField] private LvlCompleteWindow _completeWindow;
    [SerializeField] private TextMeshProUGUI _scoreValueText;
    [SerializeField] private Transform _parentResultsWindow;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _retryButton;
    
    [SerializeField] private GameObject _bulletFinishedScreen;
    
    private GameStateMachine _gameStateMachine;
    
    private LevelContext _levelContext;
    private LevelDispatcher _levelDispatcher;
    private RewardCoordinator _rewardCoordinator;

    public void Construct(LevelContext levelContext, LevelDispatcher levelDispatcher, RewardCoordinator rewardCoordinator)
    {
        _rewardCoordinator = rewardCoordinator;
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
    }

    public void SetGameStateMachine(GameStateMachine gameStateMachine) => 
        _gameStateMachine = gameStateMachine;

    public void Run()
    {
        _completeWindow.Construct(_gameStateMachine);
        _bulletField.AddBullet(_levelContext.Player.GunView.GetBulletCount());

        _menuButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadMenuState>());
        _retryButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState>(SceneNavigator.GetCurrentLvlName()));
    }
    
    public void SetRewardScore(int value)
    {
        _scoreValueText.text = value.ToString();
        _completeWindow.SetScore(value);
    }

    public void ShowCompleteWindow()
    {
        _parentResultsWindow.gameObject.SetActive(true);
        _completeWindow.gameObject.SetActive(true);
    }
    
    private void ShowBulletFinishedScreen() => 
        _bulletFinishedScreen.SetActive(true);
}