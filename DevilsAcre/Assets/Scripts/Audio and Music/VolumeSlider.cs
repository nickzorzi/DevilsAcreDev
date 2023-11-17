using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;


    void Start()
    {
        SoundManager.Instance.ChangeMasterVolume(_slider.value);

        // takes current value of slider position and pushes it into Manager to change volume
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
    }


}
