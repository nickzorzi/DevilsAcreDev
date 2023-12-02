using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEntry : MonoBehaviour
{
    public bool isFading;
    public SpriteRenderer gateRenderer;
    private float delay = 0.1f;

    public IEnumerator Fade()
    {
        isFading = true;

        for(float i = gateRenderer.color.a; i > 0 ; i--)
        {
            Color temp = gateRenderer.color;
            temp.a = i;
            gateRenderer.color = temp;
            yield return new WaitForSeconds(delay);

        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && PlayerData.Instance.hasKey && !isFading)
        {
            StartCoroutine(Fade());
            
        }
    }
}
