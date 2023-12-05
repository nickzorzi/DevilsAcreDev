using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHeartsFire : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 90f, endAngle = 270f;

    public float fireCooldown = 2f;
    public float HeartDistance = 1.2f;
    [Space(10)]
    [SerializeField] private Transform pivot;

    [Space(10)]
    [SerializeField] private AudioClip redShootEffect;
    [SerializeField] private AudioClip redLaunchEffect;

    [HideInInspector] public bool isShooting;
    private SisterBoss sisterBoss;
    void Start()
    {
        // InvokeRepeating("Fire", 0f, 2f);
        sisterBoss = GetComponent<SisterBoss>();
    }

    void Update()
    {
        if (sisterBoss.canFireRed == true)
        {
            if (!isShooting)
            {
                StartCoroutine(FireRedHearts());
            }
        }
    }

    private IEnumerator FireRedHearts()
    {
        isShooting = true;
        pivot.rotation = Quaternion.Euler(0f, 0f, 0f);
        int offset = Random.Range(10, 40);

        List<GameObject> bul = new List<GameObject>();

        SoundManager.Instance.PlaySound(redShootEffect);

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            bul.Add(BulletPoolRed.bulletPoolInstance.GetBullet());
            pivot.rotation = Quaternion.Euler(0, 0, angle);
            bul[i].SetActive(true);
            bul[i].transform.position = pivot.position + Vector3.down*HeartDistance;
            // bul[i].transform.rotation = Quaternion.Euler(0, 0, angle+180); 
            bul[i].transform.parent = pivot;
            angle += angleStep;
        }


        for(int i = 0; i < offset; i++)
        {
            yield return new WaitForSeconds(.05f);
            pivot.Rotate(0, 0, Mathf.Abs(Mathf.Sin(i)*10));
        }
        
        

        foreach (GameObject bullet in bul)
        {
            // yield return new WaitForSeconds(.1f);
            bullet.GetComponent<RedHearts>().isMoving = true;
        }

        pivot.DetachChildren();
        SoundManager.Instance.PlaySound(redLaunchEffect);
        yield return new WaitForSeconds(fireCooldown);
        isShooting = false;
    }
}
