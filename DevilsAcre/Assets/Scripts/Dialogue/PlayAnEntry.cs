using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnEntry : MonoBehaviour
{
    [SerializeField] private string ThisID;
    [SerializeField] private DialogueEntry entry;
    [SerializeField] private GameObject[] turnOffItemsDuring;

    [HideInInspector] public bool alreadyPlayed;

    private void Start()
    {
        for(int i = 0; i < PlayerData.Instance.dialogues.Length; i++)
        {
            if (PlayerData.Instance.dialogues[i].name == ThisID && PlayerData.Instance.dialogues[i].hasPlayed) {
                alreadyPlayed = true;
                gameObject.SetActive(false);
                return;
            }

        }

        
        OnandOff();
        DialogueDisplay.Instance.TurnOnDialogue(entry);
    }


    private void Update()
    {
        if (!DialogueDisplay.Instance.isRunning)
        {
            OnandOff();
            for (int i = 0; i < PlayerData.Instance.dialogues.Length; i++)
            {
                if (PlayerData.Instance.dialogues[i].name == ThisID)
                {
                    PlayerData.Instance.dialogues[i].hasPlayed = true;
                    break;
                }

            }
            gameObject.SetActive(false);
            
        }
    }


    private void OnandOff()
    {
        foreach (GameObject obj in turnOffItemsDuring)
        {


            obj.SetActive(!obj.activeSelf);

        }
    }
}
