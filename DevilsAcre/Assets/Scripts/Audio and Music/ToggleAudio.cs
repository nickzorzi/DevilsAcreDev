using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleEffects;


    // Toggles Music or Effects depending on what the game object sets for its boolean
    // (Can be both if you are crazy)
    public void Toggle()
    {
        if(_toggleEffects) SoundManager.Instance.ToggleEffects();
        if(_toggleMusic) SoundManager.Instance.ToggleMusic();
    }

}
