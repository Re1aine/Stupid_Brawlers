using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlCompleteWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreValueText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _menuButton;
    
    private GameStateMachine _gameStateMachine;

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    private void Awake()
    {
        string nextLvlSceneName = SceneNavigator.GetNextLvlScene();
        _menuButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadMenuState>());
        _nextButton.onClick.AddListener(() =>_gameStateMachine.Enter<LoadLevelState>(nextLvlSceneName));
    }

    public void SetScore(int value) => _scoreValueText.text = value.ToString();
}