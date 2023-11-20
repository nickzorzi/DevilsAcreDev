using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHeartsFire : MonoBehaviour
{
    private float angle = 0f;

    public SisterBoss sisterBoss;

    public bool isShooting = false;

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

        for (int i = 0; i < 50; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPoolYellow.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<YellowHearts>().SetMoveDirection(bulDir);

            angle += 20f;

            yield return new WaitForSeconds(8/10);
        }
        
        yield return new WaitForSeconds(5);

        isShooting = false;
    }

}
