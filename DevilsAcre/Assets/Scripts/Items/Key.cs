using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        waveManager.keyGrab = false;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            waveManager.keyGrab = true;
            Destroy(gameObject);
        }
    }
}
