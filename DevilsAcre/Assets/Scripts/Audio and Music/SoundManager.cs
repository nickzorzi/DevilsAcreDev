using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource, newMusicSource;

    void Start()
    {
        SetMusic(newMusicSource);
    }

    private void Awake()
    {
        // Singleton Paradox Killer
        #region SINGLETON
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Duplicate AudioManager Detected -- Deleting Duplicate...");
            Destroy(gameObject);
        }
        #endregion

        // _musicSource.ignoreListenerPause = true;
        _effectsSource.ignoreListenerPause = true;
    }

    // Called to Play the Effect Sound
    // (Kinda Self-Explanitory)
    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public float GetEffectVolume()
    {
        return _effectsSource.volume;
    }

    public bool CheckForMute()
    {
        return _effectsSource.mute;
    }
    
    public bool CheckForPlaying()
    {
        return _effectsSource.isPlaying;
    }

    public float GetMusicVolume()
    {
        return _musicSource.volume;
    }

    public float GetMasterVolume()
    {
        return AudioListener.volume;
    }


    #region #### SLIDER CONTROLS ####

    // slider function to change all audio volumes
    public void MasterVolumeSlider(float value)
    {
        AudioListener.volume = value;
    }
    public void MusicVolumeSlider(float value)
    {
        _musicSource.volume = value;

    }
    public void EffectsVolumeSlider(float value)
    {
        _effectsSource.volume = value;
    }

    #endregion

    #region #### BUTTON CONTROLS ####
    
    // Mutes or Unmutes audios
    public void ToggleMaster()
    {
        ToggleEffects();
        ToggleMusic();
    }
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    #endregion

    public void gamePaused()
    {
        _musicSource.pitch = .8f;
    }

    public void gameUnPaused()
    {
        _musicSource.pitch = 1;
    }

    public void SetMusic(AudioSource newMusicSource)
    {
        // Stop the current music
        _musicSource.Stop();

        // Copy relevant properties from the new AudioSource to the existing one
        _musicSource.clip = newMusicSource.clip;
        _musicSource.volume = newMusicSource.volume;
        _musicSource.pitch = newMusicSource.pitch;
        _musicSource.loop = newMusicSource.loop;

        // Play the new music
        _musicSource.Play();
    }
}