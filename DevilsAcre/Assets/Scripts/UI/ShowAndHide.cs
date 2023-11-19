using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{

    [SerializeField] private GameObject targetUI;
    [SerializeField] private AudioClip buttonSFX;

    public void ShowAndHideUI()
    {
        if(buttonSFX != null)
        {
            SoundManager.Instance.PlaySound(buttonSFX);
        }
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
