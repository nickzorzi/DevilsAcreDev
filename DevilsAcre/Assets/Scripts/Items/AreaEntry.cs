using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntry : MonoBehaviour
{
    [SerializeField] private GameObject DialogueStart;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            DialogueStart.SetActive(true);
            Destroy(gameObject);
        }
    }
}
