using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDialoguePosition : MonoBehaviour
{
    [SerializeField] private string selectedScene;
    [SerializeField] private PlayAnEntry turnThisOn;

    private void Start()
    {
        if(selectedScene == PlayerData.Instance.lastScene)
        {
            turnThisOn.enabled = true;
        }
    }
}
