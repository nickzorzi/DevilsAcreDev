using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    [SerializeField] private AudioClip enemyHitSoundEffect;

    [SerializeField] private HitFlash hitFlash;

    private Transform pos;

    private void Start()
    {
        pos = transform; 
    }

    private IEnumerator ShakeOnHit()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                transform.Translate(new Vector2(-0.05f, 0));
            }
            else
            {
                transform.Translate(new Vector2(0.05f, 0));
            }
            yield return new WaitForSeconds(0.1f);
        }

        transform.position = pos.position; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) 
        {
            SoundManager.Instance.PlaySound(enemyHitSoundEffect);
            hitFlash.Flash();
            StartCoroutine(ShakeOnHit());
        }
    }
}
