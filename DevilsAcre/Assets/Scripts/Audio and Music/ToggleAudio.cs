using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{

    [Header("Optional Icons")]
    [SerializeField] private Sprite unMuteIcon;
    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Image _icon;
    [Space(10)]
    [SerializeField] private toggles toggleThis;

    private bool _isMuted = false;


    // Toggles Music or Effects depending on what the game object sets for its boolean
    // (Can be both if you are crazy)
    public void Toggle()
    {
        if (toggleThis == toggles.Effects) SoundManager.Instance.ToggleEffects();
        else if (toggleThis == toggles.Music) SoundManager.Instance.ToggleMusic();
        else SoundManager.Instance.ToggleMaster();

        if(unMuteIcon != null && muteIcon != null) // only run if there are sprites inserted
        {
            if(_isMuted)
            {
                _icon.sprite = unMuteIcon;
                _isMuted = false; 
            }
            else
            {
                _icon.sprite = muteIcon;
                _isMuted = true;
            }
        }

    }
    public enum toggles
    {
        Master,
        Music,
        Effects
    }
}
