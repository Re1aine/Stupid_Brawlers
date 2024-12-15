using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
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

    public void Construct(LevelContext levelContext, LevelDispatcher levelDispatcher)
    {
        _levelContext = levelContext;
        _levelDispatcher = levelDispatcher;
    }

    public void SetGameStateMachine(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Run()
    {
        _bulletField.AddBullet(_levelContext.Player.GunView.GetBulletCount());
        
        _menuButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadMenuState>());
        _retryButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState>(SceneNavigator.GetCurrentLvlName()));
        
        _completeWindow.Construct(_gameStateMachine);
        
        _levelDispatcher.OnPlayerShooted += _bulletField.SubtractBullet;
        _levelDispatcher.OnRewardPointsAssigned += SetScoreValue;
        _levelDispatcher.OnAllRewardPointsAssigned += _completeWindow.SetScore;
        
        _levelDispatcher.OnLevelCompleted += ShowCompleteWindow;

       // _levelDispatcher.OnAllBulletsFinished += ShowBulletFinishedScreen;
    }

    private void OnDisable()
    {
        _levelDispatcher.OnPlayerShooted -= _bulletField.SubtractBullet;
        _levelDispatcher.OnRewardPointsAssigned -= SetScoreValue;
        _levelDispatcher.OnAllRewardPointsAssigned -= _completeWindow.SetScore;
        
        _levelDispatcher.OnLevelCompleted -= ShowCompleteWindow;
        
        //_levelDispatcher.OnAllBulletsFinished += ShowBulletFinishedScreen;
    }

    private void ShowCompleteWindow()
    {
        _parentResultsWindow.gameObject.SetActive(true);
        _completeWindow.gameObject.SetActive(true);
    }
    
    private void SetScoreValue(int scoreValue) => 
        _scoreValueText.text = scoreValue.ToString();

    private void ShowBulletFinishedScreen()
    {
        _bulletFinishedScreen.SetActive(true);
    }
}