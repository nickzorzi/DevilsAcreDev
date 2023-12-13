using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    
    [SerializeField] private AudioClip song;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.SetMusic(song);
    }
}
