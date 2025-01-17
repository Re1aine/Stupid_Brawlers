using UnityEngine;

public class AudioService
{
    private readonly GameFactory _gameFactory;
    private AudioPlayer _audioPlayer;

    public AudioService(GameFactory gameFactory)
    {
        _gameFactory = gameFactory;

        _audioPlayer = _gameFactory.CreateAudioPlayer();
    }

    public void Initialize()
    {
        _audioPlayer = _gameFactory.CreateAudioPlayer();
        var backgroundAudioClip = Resources.Load<AudioClip>("music");
        PlayBackGroundMusic(backgroundAudioClip);
    }

    public void PlayBackGroundMusic(AudioClip clip) => 
        _audioPlayer.PlayOnBackGroundPlayer(clip);

    public void PlayShortSound(AudioClip clip) => 
        _audioPlayer.PlayOnShortSoundEffectPlayer(clip);

    public void StopBackGroundMusic() => 
        _audioPlayer.StopBackGroundMusic();

    public void StopShortSoundEffects() => 
        _audioPlayer.StopShortSoundEffects();

    public void SetGeneralVolume(float volume) => 
        _audioPlayer.SetGeneralVolume(volume);

    public void SetVolumeBackGroundMusic(float volume) => 
        _audioPlayer.SetVolumeBackGroundMusic(volume);

    public void SetVolumeShortSoundEffect(float volume) => 
        _audioPlayer.SetVolumeShortSoundEffect(volume);
}