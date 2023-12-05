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
            int topLine = Random.Range(0, 2); // 0 or 2
            List<GameObject> hearts = new List<GameObject>();

            if (topLine == 0) // top line
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
            else // bottom line
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

            yield return new WaitForSeconds(1f);
            foreach (GameObject bullet in hearts)
            {
                bullet.GetComponent<RedHearts>().isMoving = true;
            }
            yield return new WaitForSeconds(inBetweenDelay);
        }
        yield return new WaitForSeconds(3f); // cool down
        isFiring = false;
    }

}
