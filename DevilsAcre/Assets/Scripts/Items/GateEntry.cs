using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEntry : MonoBehaviour
{
    private bool isFading;
    [SerializeField] private float fadeSpeed = .2f;
    [SerializeField] private AudioClip unlockSFX;
    IEnumerator fade()
    {
        isFading = true;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(fadeSpeed);
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.a -= .1f;
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && PlayerData.Instance.hasKey && !isFading)
        {
            StartCoroutine(fade());

            if(unlockSFX != null)
            {
            SoundManager.Instance.PlaySound(unlockSFX);
            }
        }
    }
}


