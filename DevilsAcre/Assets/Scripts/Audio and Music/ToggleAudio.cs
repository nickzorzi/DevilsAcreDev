using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public enum toggles
    {
        Master,
        Music,
        Effects
    }
    [SerializeField] private toggles toggleThis;

    // Toggles Music or Effects depending on what the game object sets for its boolean
    // (Can be both if you are crazy)
    public void Toggle()
    {
        if (toggleThis == toggles.Effects) SoundManager.Instance.ToggleEffects();
        else if (toggleThis == toggles.Music) SoundManager.Instance.ToggleMusic();
        else SoundManager.Instance.ToggleMaster();
    }

}
