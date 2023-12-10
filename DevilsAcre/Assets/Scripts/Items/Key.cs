using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject DialogueStart;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayerData.Instance.hasKey = true;
            DialogueStart.SetActive(true);
            Destroy(gameObject);
        }
    }
}
