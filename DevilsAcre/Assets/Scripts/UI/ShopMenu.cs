using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public static bool ShopIsOpen = false;
    public static bool inRange = false;

    public GameObject shopMenuUI;
    public GameObject shopNotiUI;

    [Header("Audio")]
    [SerializeField] private AudioClip shopOpenSound;
    [SerializeField] private AudioClip shopCloseSound;




    void Update()
    {
        if (inRange)
        {
            shopNotiUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (ShopIsOpen)
                {
                    CloseShop();
                }
                else
                {
                    OpenShop();
                }
            }
        }
        if (!inRange)
        {
            shopNotiUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void CloseShop()
    {
        shopMenuUI.SetActive(false);
        ShopIsOpen = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SoundManager.Instance.PlaySound(shopCloseSound);
    }

    void OpenShop()
    {
        shopMenuUI.SetActive(true);
        ShopIsOpen = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        SoundManager.Instance.PlaySound(shopOpenSound);
    }
}
