using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;
    private void Awake()
    {
        // Singleton Paradox Killer
        #region SINGLETON
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate AudioManager Detected -- Deleting Duplicate...");
            Destroy(gameObject);
        }
        #endregion

    }

    // Called to Play the Effect Sound
    // (Kinda Self-Explanitory)
    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }



    // #### OPTION CONTROLS ####

    // slider function to change all audio volumes
    // can be used by button as well
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }


    // Mutes or Unmutes audio for Effects
    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    // Music
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public void gamePaused()
    {
        _musicSource.pitch = .8f;
    }

    public void gameUnPaused()
    {
        _musicSource.pitch = 1;
    }
}