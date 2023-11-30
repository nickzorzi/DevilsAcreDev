using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject rWave1;
    public GameObject rWave2;
    public GameObject rWave3;

    public GameObject pWave1;
    public GameObject pWave2;
    public GameObject pWave3;

    public GameObject mWave1;
    public GameObject mWave2;
    public GameObject mWave3;

    public Score scoreManager;

    public GameObject gKey;
    public bool keyGrab = false;

    public GameObject loadZone;

    void Start()
    {
        rWave1.SetActive(true);
        rWave2.SetActive(false);
        rWave3.SetActive(false);

        pWave1.SetActive(true);
        pWave2.SetActive(false);
        pWave3.SetActive(false);

        mWave1.SetActive(true);
        mWave2.SetActive(false);
        mWave3.SetActive(false);

        gKey.SetActive(false);
        
        keyGrab = false;

        loadZone.SetActive(false);
    }

    void Update()
    {
        if (Score.scoreValue == 35)
        {
            rWave1.SetActive(false);
            rWave2.SetActive(true);

            pWave1.SetActive(false);
            pWave2.SetActive(true);

            mWave1.SetActive(false);
            mWave2.SetActive(true);


            if (keyGrab)
            {
                loadZone.SetActive(true);
            }
            else
            {
            gKey.SetActive(true);
            }
        }
        else if (Score.scoreValue == 1720)
        {
            rWave2.SetActive(false);
            rWave3.SetActive(true);

            pWave2.SetActive(false);
            pWave3.SetActive(true);

            mWave2.SetActive(false);
            mWave3.SetActive(true);
        }
    }
}
