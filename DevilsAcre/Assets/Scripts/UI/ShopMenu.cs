using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public static bool ShopIsOpen = false;

    public GameObject shopMenuUI;

    [SerializeField] private AudioSource shopOpenSound;
    [SerializeField] private AudioSource shopCloseSound;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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

    public void CloseShop()
    {
        shopMenuUI.SetActive(false);
        ShopIsOpen = false;

        shopCloseSound.Play();
    }

    void OpenShop()
    {
        shopMenuUI.SetActive(true);
        ShopIsOpen = true;

        shopOpenSound.Play();
    }
}
