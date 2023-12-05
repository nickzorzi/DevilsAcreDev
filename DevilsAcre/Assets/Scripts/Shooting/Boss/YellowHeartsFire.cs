using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHeartsFire : MonoBehaviour
{
    private float angle = 0f;
    public float fireRate = 0.025f;

    public SisterBoss sisterBoss;

    public bool isShooting = false;

    [SerializeField] private AudioClip yellowShootEffect;

    void Start()
    {
        sisterBoss = GetComponent<SisterBoss>();
    }

    void Update()
    {
        if (sisterBoss.canFireYellow == true)
        {
            if (!isShooting)
            {
                StartCoroutine(FireYellowHearts());
                CinemachineShake.Instance.ShakeCamera(2f, 1f); //Camera Shake
            }
        }
    }

    private IEnumerator FireYellowHearts()
    {
        isShooting = true;

        SoundManager.Instance.PlaySound(yellowShootEffect);

        for (int i = 0; i < 50; i++)
        {
            GameObject bul = BulletPoolYellow.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.transform.rotation = Quaternion.Euler(0, 0, angle);
            angle += 20f;

            yield return new WaitForSeconds(fireRate);
        }
        
        yield return new WaitForSeconds(3);

        isShooting = false;
    }

}
