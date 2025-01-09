using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LvlSlotView : MonoBehaviour
{
    [SerializeField] private int _level;
    
    private GameStateMachine _gameStateMachine;

    private LevelSlotState _state;
    private Button _button;

    [SerializeField] private GameObject _text;
    [SerializeField] private Image _backgroundBox;
    [SerializeField] private Sprite _lockedBackgroundBox;
    [SerializeField] private Sprite _unlockedBackgroundBox;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadLevel);
        LockSlot();
    }

    public void LockSlot()
    {
        _text.SetActive(false);
        _backgroundBox.sprite = _lockedBackgroundBox;
        _state = LevelSlotState.Locked;
    }

    public void UnlockSlot()
    {
        _text.SetActive(true);
        _backgroundBox.sprite = _unlockedBackgroundBox;
        _state = LevelSlotState.UnLocked;
    }
    
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    private void LoadLevel()
    {
        if(_state != LevelSlotState.UnLocked) return;
        
        string levelName = SceneNavigator.GetLvlSceneNameByIndex(_level);
        _gameStateMachine.Enter<LoadLevelState>(levelName);
    }
}

public enum LevelSlotState
{
    Locked = 0,
    UnLocked = 1
}