using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Transform _content;
    
    private List<LvlSlotView> _lvlSlots;

    private GameStateMachine _gameStateMachine;
    
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        
        CreateLvlSlotView();
    }

    private void Awake()
    {
        var firstLvlSceneName = SceneNavigator.GetFirstLvlName();
        _startButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState>(firstLvlSceneName));
    }

    private void CreateLvlSlotView()
    {
        var length = 0;

        foreach (Transform child in _content)
            if (child.GetComponent<LvlSlotView>() != null)
                length += 1;
                
        
        _lvlSlots = new List<LvlSlotView>(length);

        foreach (Transform child in _content) 
            _lvlSlots.Add(child.GetComponent<LvlSlotView>());
        
        foreach (LvlSlotView lvlSlot in _lvlSlots) 
            lvlSlot.Construct(_gameStateMachine);
    }
}

