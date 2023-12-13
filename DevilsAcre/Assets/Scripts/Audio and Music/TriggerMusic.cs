using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    [SerializeField] private AudioClip song;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            SoundManager.Instance.SetMusic(song);
            Destroy(gameObject);
        }
    }
}
