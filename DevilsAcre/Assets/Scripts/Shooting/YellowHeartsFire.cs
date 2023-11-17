using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHeartsFire : MonoBehaviour
{
    private float angle = 0f;

    private Vector2 bulletMoveDirection;

    public SisterBoss sisterBoss;

    public bool isShooting = false;

    void Start()
    {
        // InvokeRepeating("Fire", 0f, 2f);
        sisterBoss = GetComponent<SisterBoss>();
    }

    void Update()
    {
        if (sisterBoss.canFireYellow == true)
        {
            if (!isShooting)
            {
                StartCoroutine(FireYellowHearts());
            }
        }
    }

    private IEnumerator FireYellowHearts()
    {
        isShooting = true;

        for (int i = 0; i <= 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin(((angle * 180f * i) * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos(((angle * 180f * i) * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<YellowHearts>().SetMoveDirection(bulDir);
        }

        angle += 10f;

        if(angle >= 360f)
        {
            angle = 0f;
        }

        yield return null;

        isShooting = false;
    }
}
