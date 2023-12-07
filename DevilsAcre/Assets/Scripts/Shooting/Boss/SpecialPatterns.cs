using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPatterns : MonoBehaviour
{
    public enum patterns
    {
        line,
        crissCrossLR,
        crissCrossTB,
    }

    [SerializeField] private Transform centerPivot;
    [SerializeField] private Transform LeftPivot;
    [SerializeField] private Transform RightPivot;
    [SerializeField] private GameObject warningLine;

    [Space(10)]
    [SerializeField] private AudioClip redShootEffect;
    [SerializeField] private AudioClip redLaunchEffect;

    private bool isFiring;
    public void intiatePattern(patterns selectedPattern, int amount, float inBetweenDelay)
    {
        if(!isFiring)
        {
            switch(selectedPattern)
            {
                case patterns.line:
                    StartCoroutine(linePattern(amount, inBetweenDelay));
                    break;
                case patterns.crissCrossLR:
                    break;
                case patterns.crissCrossTB:
                    break;
            }
        }
    }


    private IEnumerator linePattern(int amount, float inBetweenDelay)
    {
        isFiring = true;
        for(int i = 0; i < amount; i++)
        {
            int DetermineLine = Random.Range(0, 5); // 0 or 4
            List<GameObject> hearts = new List<GameObject>();
            SoundManager.Instance.PlaySound(redShootEffect);

            if (DetermineLine == 0) // top line
            {
                for( int j = 0; j < 9; j++)
                {
                    hearts.Add(BulletPoolRed.bulletPoolInstance.GetBullet());
                    hearts[j].transform.position = LeftPivot.transform.position + (Vector3.right*2)*j + Vector3.right;
                    hearts[j].SetActive(true);
                    hearts[j].transform.rotation = Quaternion.Euler(0f, 0f, 0);
                    GameObject temp = Instantiate(warningLine);
                    temp.transform.position = LeftPivot.transform.position + (Vector3.right * 2) * j + Vector3.right;
                    temp.transform.rotation = Quaternion.Euler(0f, 0f, 0);
                    Destroy(temp, 1f);
                }

            }
            else if(DetermineLine == 1)// bottom line
            {
                for (int j = 0; j < 9; j++)
                {
                    hearts.Add(BulletPoolRed.bulletPoolInstance.GetBullet());
                    hearts[j].transform.position = RightPivot.transform.position + (Vector3.left * 2) * j + Vector3.left*.5f;
                    hearts[j].SetActive(true);
                    hearts[j].transform.rotation = Quaternion.Euler(0f, 0f, 180);
                    GameObject temp = Instantiate(warningLine);
                    temp.transform.position = RightPivot.transform.position + (Vector3.left * 2) * j + Vector3.left*.5f;
                    temp.transform.rotation = Quaternion.Euler(0f, 0f, 180);
                    Destroy(temp, 1f);
                }
            }
            else if (DetermineLine == 2)// Right line
            {
                for (int j = 0; j < 5; j++)
                {
                    hearts.Add(BulletPoolRed.bulletPoolInstance.GetBullet());
                    hearts[j].transform.position = RightPivot.transform.position + (Vector3.up * 2) * j + Vector3.up * .5f;
                    hearts[j].SetActive(true);
                    hearts[j].transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    GameObject temp = Instantiate(warningLine);
                    temp.transform.position = RightPivot.transform.position + (Vector3.up * 2) * j + Vector3.up * .5f;
                    temp.transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    Destroy(temp, 1f);
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    hearts.Add(BulletPoolRed.bulletPoolInstance.GetBullet());
                    hearts[j].transform.position = LeftPivot.transform.position + (Vector3.down * 2) * j + Vector3.down * .5f;
                    hearts[j].SetActive(true);
                    hearts[j].transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    GameObject temp = Instantiate(warningLine);
                    temp.transform.position = LeftPivot.transform.position + (Vector3.down * 2) * j + Vector3.down * .5f;
                    temp.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    Destroy(temp, 1f);
                }
            }

            yield return new WaitForSeconds(1f);
            foreach (GameObject bullet in hearts)
            {
                bullet.GetComponent<RedHearts>().isMoving = true;
            }
            SoundManager.Instance.PlaySound(redLaunchEffect);
            yield return new WaitForSeconds(inBetweenDelay);
        }
        yield return new WaitForSeconds(3f); // cool down
        isFiring = false;
    }

}
