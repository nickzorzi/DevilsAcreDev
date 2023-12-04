using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{

    [SerializeField] private GameObject targetUI;

    private AudioSource effectSource;


    public void ShowAndHideUI()
    {

        targetUI.SetActive(!targetUI.activeSelf);
    }

    // TEMPERARY DELETE SOON
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            targetUI.SetActive(false);
        }
    }
}
