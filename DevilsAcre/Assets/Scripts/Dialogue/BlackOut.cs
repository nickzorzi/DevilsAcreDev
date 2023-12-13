using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{


    private Color prevColor;

    private void OnEnable()
    {
        prevColor = DialogueDisplay.Instance.DialogueUI.GetComponent<Image>().color;
        DialogueDisplay.Instance.DialogueUI.GetComponent<Image>().color = Color.black;
    }


    private void OnDisable()
    {
        DialogueDisplay.Instance.DialogueUI.GetComponent<Image>().color = prevColor;
    }
}
