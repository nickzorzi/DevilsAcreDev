using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    public static DialogueDisplay Instance;
    
    public float delay = 0.1f;

    [SerializeField] private TextMeshProUGUI DisplayText;
    [SerializeField] private GameObject DialogueUI;
    
    private DialogueEntry currentEntry;
    private int currentlyReading = 0;
    private string currentText = "";
    private bool textIsRunning = false;
    private float speed;

    private void Awake()
    {
        // Singleton Paradox Killer
        #region SINGLETON
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate DialogueDisplay Detected -- Deleting Duplicate...");
            Destroy(gameObject);
        }
        #endregion
    }

    public void TurnOnDialogue(DialogueEntry entry)
    {
        DialogueUI.gameObject.SetActive(true);
        currentEntry = entry;
        currentlyReading = 0;
        currentText = "";
        StartCoroutine(ShowText());
    }
    
    public void CloseDialogue()
    {
        DialogueUI.gameObject.SetActive(false);
    }

    IEnumerator ShowText()
    {
        textIsRunning = true;
        speed = delay;
        for(int i = 0; i < currentEntry.entries[currentlyReading].Length+1; i++)
        {
            currentText = currentEntry.entries[currentlyReading].Substring(0, i);
            DisplayText.text = currentText;
            yield return new WaitForSeconds(speed);

        }
        textIsRunning = false;
    }

    public void playNext()
    {
        if(textIsRunning)
        {
            speed = 0.01f;
            return;
        }
        else
        {
            currentlyReading++;
        }
        if(currentlyReading > currentEntry.entries.Length-1)
        {
            CloseDialogue();
            return;
        }
        StartCoroutine(ShowText());
    }
}
