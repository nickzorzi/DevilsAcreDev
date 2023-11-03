using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static bool allowMusic = true;
    
    [SerializeField] private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        allowMusic = true;
        backgroundMusic.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.scoreValue >= 1090 && allowMusic == true) //1090 default
        {
            backgroundMusic.Pause();
            allowMusic = false;
        }
    }
}
