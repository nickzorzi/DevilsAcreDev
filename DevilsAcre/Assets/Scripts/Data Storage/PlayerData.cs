using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{

    public static PlayerData Instance;


    

    [Header("Player Info")]
    public int currentHealth;
    [Space(10)]
    [Header("Other Data")]
    public string lastScene;
    

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
            Debug.Log("Duplicate AudioManager Detected -- Deleting Duplicate...");
            Destroy(gameObject);
        }
        #endregion
    }




}
