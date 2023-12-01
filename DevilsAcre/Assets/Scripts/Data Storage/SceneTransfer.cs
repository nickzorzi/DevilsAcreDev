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
    [SerializeField] private bool updatePlayerHealth = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(updatePlayerHealth)
            {
                var temp = collision.GetComponent<PlayerController>();
                PlayerData.Instance.currentHealth = temp.currentHealth;
            }

            PlayerData.Instance.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(loadScene.ToString());
        }
    }



}
