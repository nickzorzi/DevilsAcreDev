using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPausePause : MonoBehaviour
{

    [SerializeField] private ShopMenu shopScript;
    [SerializeField] private PlayAnEntry playAnEntry;




    private void Update()
    {
        if (PlayerData.Instance.canAxe || PlayerData.Instance.canMolotov)
        {
            playAnEntry.enabled = true;
            if(!playAnEntry.alreadyPlayed)
            {
                shopScript.CloseShop();

            }
                Destroy(GetComponent<UnPausePause>());
        }
    }


 


}
