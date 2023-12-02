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
        hasKey = false;
        canDash = false;
        canAxe = false;
        canMolotov = false;
        canDoubleEdged = false;
        canQuickfire = false;
    }


}
