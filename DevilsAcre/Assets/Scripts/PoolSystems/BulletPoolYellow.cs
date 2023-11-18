using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolYellow : MonoBehaviour
{
    public static BulletPoolYellow bulletPoolInstance;
    [SerializeField] private GameObject pooledBullet;
    [SerializeField] private bool notEnoughBulletsInPool = true;
    public int amountToPool;
    private List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(pooledBullet);
            tmp.SetActive(false);
            bullets.Add(tmp);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }

}
