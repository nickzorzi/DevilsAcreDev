using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCaller : MonoBehaviour
{
    public DialogueEntry entry;
    // Start is called before the first frame update
    void Start()
    {
 
            DialogueDisplay.Instance.TurnOnDialogue(entry);
        
    }


}
