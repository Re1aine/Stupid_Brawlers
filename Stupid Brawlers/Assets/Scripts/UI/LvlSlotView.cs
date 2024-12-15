using UnityEngine;
using UnityEngine.UI;

public class LvlSlotView : MonoBehaviour
{
    [SerializeField] private int _level;

    private GameStateMachine _gameStateMachine;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadLevel);
    }

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    private void LoadLevel()
    {
        string levelName = SceneNavigator.GetLvlSceneNameByIndex(_level);
        _gameStateMachine.Enter<LoadLevelState>(levelName);
    }
}