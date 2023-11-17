using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHeartsFire : MonoBehaviour
{
    public SisterBoss sisterBoss;

    public GameObject bullet;
    public GameObject bulletParent;

    public bool isShooting;

    void Start()
    {
        // InvokeRepeating("Fire", 0f, 2f);
        sisterBoss = GetComponent<SisterBoss>();

        isShooting = false;
    }

    void Update()
    {
        if (sisterBoss.canFireBlue == true)
        {
            if (!isShooting)
            {
                StartCoroutine(FireBlueHearts());
            }
        }
    }

    private IEnumerator FireBlueHearts()
    {
        isShooting = true;

        Instantiate(bullet,bulletParent.transform.position, Quaternion.identity);

        yield return null;
    }
}
