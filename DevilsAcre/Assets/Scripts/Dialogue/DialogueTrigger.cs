using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] private string ThisID;
    [SerializeField] private DialogueEntry entry;
    [SerializeField] private GameObject[] turnOffItemsDuring;

    private bool needsOpen = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerData.Instance.hasKey && needsOpen)
        {
            DialogueTriggered();
            needsOpen = false;
        }



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

    void DialogueTriggered()
    {
        for(int i = 0; i < PlayerData.Instance.dialogues.Length; i++)
        {
            if (PlayerData.Instance.dialogues[i].name == ThisID && PlayerData.Instance.dialogues[i].hasPlayed) {
                gameObject.SetActive(false);
                return;
            }

        }

        
        OnandOff();
        DialogueDisplay.Instance.TurnOnDialogue(entry);
    }

    private void OnandOff()
    {
        foreach (GameObject obj in turnOffItemsDuring)
        {
            if(obj.GetComponent<PlayerController>())
            {
                obj.GetComponent<PlayerController>().enabled = !obj.GetComponent<PlayerController>().enabled;
                continue;
            }

            obj.SetActive(!obj.activeSelf);

        }
    }
}
