using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public enum SceneNames
    {
        Saloon,
        Shop,
        Town,
        Graveyard,
        Tutorial,
        MainMenu,
        Credits

    }

    [SerializeField] private SceneNames loadScene;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData.Instance.lastScene = SceneManager.GetActiveScene().name;
            // Debug.Log(loadScene.ToString());
            SceneManager.LoadScene(loadScene.ToString());
        }
    }



}
