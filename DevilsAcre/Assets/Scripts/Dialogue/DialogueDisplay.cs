using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public static DialogueDisplay Instance;
    public float delay = 0.1f;
    [Header("UI Items")]
    [SerializeField] private TextMeshProUGUI DisplayText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private GameObject characterIcon;
    [Header("UI Container")]
    [SerializeField] private GameObject DialogueUI;
    [Header("Sound Effects")]
    [SerializeField] private int textSFXSpacing = 1;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip textSFX;
    [SerializeField] private AudioClip buttonSFX;


    [HideInInspector] public bool isRunning = false;
    private DialogueEntry currentEntry;
    private int currentlyReading = 0;
    private string currentText = "";
    private bool textIsRunning = false;
    private int textSFXSpeed;
    private float speed;

    private void Awake()
    {
        if(source != null)
        {
            source.ignoreListenerPause = true; // plays through pause
        }

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
        if(source != null)
        {
            source.volume = SoundManager.Instance.GetEffectVolume() - 0.4f;
            source.mute = SoundManager.Instance.CheckForMute();
        }
        isRunning = true;
        DialogueUI.gameObject.SetActive(true);
        currentEntry = entry;
        currentlyReading = 0;
        currentText = "";
        checkForImageAndName();
        StartCoroutine(ShowText());
    }
    
    public void CloseDialogue()
    {
        isRunning = false;
        DialogueUI.gameObject.SetActive(false);
    }

    IEnumerator ShowText()
    {
        textIsRunning = true;
        if(source != null)
        {
            textSFXSpeed = textSFXSpacing;
        }
        speed = delay;
        for(int i = 0; i < currentEntry.entries[currentlyReading].description.Length+1; i++)
        {

            if( i % textSFXSpeed == 0 && source != null)
            {
                source.PlayOneShot(textSFX);
            }

            currentText = currentEntry.entries[currentlyReading].description.Substring(0, i);
            DisplayText.text = currentText;
            yield return new WaitForSeconds(speed);

        }
        textIsRunning = false;
    }

    public void playNext()
    {
        if(source != null)
        {
            source.PlayOneShot(buttonSFX);
        }

        if(textIsRunning)
        {
            textSFXSpeed *= 10;
            speed *= .1f;
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
        checkForImageAndName();
        StartCoroutine(ShowText());
    }

    private void checkForImageAndName()
    {
        if(NameText != null)
        {
            if(currentEntry.entries[currentlyReading].name != "")
            {
                NameText.text = currentEntry.entries[currentlyReading].name;
            }
            else
            {
                NameText.text = "Narrator";
            }
        }

        if(characterIcon != null)
        {
            if(currentEntry.entries[currentlyReading].icon != null)
            {
                characterIcon.SetActive(true);
                characterIcon.GetComponent<Image>().sprite = currentEntry.entries[currentlyReading].icon;
            }
            else
            {
                characterIcon.SetActive(false);
            }
        }     
    }
}
