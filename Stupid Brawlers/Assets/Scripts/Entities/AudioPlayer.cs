using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource BackGroundMusicPlayer => _backGroundMusicPlayer;
    public AudioSource ShortSoundEffectPlayer => _shortSoundEffectPlayer;
    
    [SerializeField] private AudioSource _backGroundMusicPlayer;
    [SerializeField] private AudioSource _shortSoundEffectPlayer;
    
    public void PlayOnBackGroundPlayer(AudioClip clip)
    {
        _backGroundMusicPlayer.clip = clip;
        _backGroundMusicPlayer.Play();
    }

    public void PlayOnShortSoundEffectPlayer(AudioClip clip)
    {
        _shortSoundEffectPlayer.clip = clip;
        _shortSoundEffectPlayer.PlayOneShot(clip);
    }
    
    public void StopBackGroundMusic() => 
        _backGroundMusicPlayer.Stop();

    public void StopShortSoundEffects() => 
        _shortSoundEffectPlayer.Stop();
    
    public void SetGeneralVolume(float volume)
    {
        _backGroundMusicPlayer.volume = volume;
        _shortSoundEffectPlayer.volume = volume;
    } 
        
    public void SetVolumeBackGroundMusic(float volume) => 
        _backGroundMusicPlayer.volume = volume;

    public void SetVolumeShortSoundEffect(float volume) => 
        _shortSoundEffectPlayer.volume = volume;
}