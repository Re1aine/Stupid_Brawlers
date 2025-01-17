
using UnityEngine;

public class GameFactory
{
    private readonly Transform _transform;
    private GameStateMachine _gameStateMachine;

    public GameFactory(Transform transform)
    {
        _transform = transform;
    }
    
    public void SetGameStateMachine(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public UIMenu CreateUIMenu() => 
        AssetProvider.Instantiate<UIMenu>(AssetDataPath.UIMenuPrefab);

    public AudioPlayer CreateAudioPlayer() => 
        AssetProvider.Instantiate<AudioPlayer>(AssetDataPath.AudioPlayer, _transform);
}