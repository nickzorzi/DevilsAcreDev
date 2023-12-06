using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{

    public static PlayerData Instance;


    

    [Header("Player Info")]
    public int currentHealth;
    public bool hasKey = false;
    public bool canDash;
    public bool canAxe;
    public bool canMolotov;
    public bool canDoubleEdged;
    public bool canQuickfire;
    [Space(10)]
    [Header("Other Data")]
    public string lastScene;
    public int lastWave = 0;
    [Space(10)]
    [Header("Dialogue Bools")]
    public DialogueID[] dialogues;
   
    
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
            Debug.Log("Duplicate PlayerData Detected -- Deleting Duplicate...");
            Destroy(gameObject);
        }
        #endregion
    }

    public void ResetBools()
    {
        currentHealth = 0;
        hasKey = false;
        canDash = false;
        canAxe = false;
        canMolotov = false;
        canDoubleEdged = false;
        canQuickfire = false;
    }

    public void ResetDialogues()
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i].hasPlayed = false;
        }
    }

    [System.Serializable]
    public struct DialogueID
    {
        public string name;
        public bool hasPlayed;
    }
}
