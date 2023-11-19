using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum options
    {
        Master,
        Music,
        Effects
    }

    [SerializeField] private Slider _slider;
    [SerializeField] private options ChangeThis;

    void Start()
    {
        if(ChangeThis == options.Master)
        {
        _slider.value = SoundManager.Instance.GetMasterVolume();
        // takes current value of slider position and pushes it into Manager to change volume
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.MasterVolumeSlider(val));
        }
        else if(ChangeThis == options.Music)
        {
            _slider.value = SoundManager.Instance.GetMusicVolume();
            // takes current value of slider position and pushes it into Manager to change volume
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.MusicVolumeSlider(val));
        }
        else
        {
            _slider.value = SoundManager.Instance.GetEffectVolume();
            // takes current value of slider position and pushes it into Manager to change volume
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.EffectsVolumeSlider(val));
        }
    }


}
