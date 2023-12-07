using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolButtonIcon : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    public void IconSwitch()
    {
        icon.SetActive(!icon.activeSelf);
    }
}
