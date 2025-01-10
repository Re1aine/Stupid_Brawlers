using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Transform _content;
    
    private List<LvlSlotView> _lvlSlots;

    private GameStateMachine _gameStateMachine;
    private LevelSaveLoadMaster _levelSaveLoadMaster = new();
    private AudioService _audioService;

    public void Construct(GameStateMachine gameStateMachine, AudioService audioService)
    {
        _gameStateMachine = gameStateMachine;
        _audioService = audioService;
        
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
        
        foreach (LvlSlotView slot in _lvlSlots) 
            slot.Construct(_gameStateMachine);
        
        for (int i = 1; i < _lvlSlots.Count; i++)
        {
            var lvlSaveKey = SceneNavigator.GetLvlSceneNameByIndex(i);
            
            var data = _levelSaveLoadMaster.GetValue(lvlSaveKey, new LevelData());
            
            if(data.GetLevelState() == LevelState.Completed)
                _lvlSlots[i].UnlockSlot();
        }
        
        _lvlSlots[0].UnlockSlot();
    }
}

