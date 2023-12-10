using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    [SerializeField] private Image UIToChange;


    private Color prevColor;





    private void OnEnable()
    {
        prevColor = UIToChange.color;
        UIToChange.color = Color.black;
    }


    private void OnDisable()
    {
        UIToChange.color = prevColor;
    }
}
