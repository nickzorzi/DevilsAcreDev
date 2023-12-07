using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatModeData : MonoBehaviour
{

    [SerializeField] private TMP_InputField coinInput;
    [SerializeField] private TMP_InputField healthInput;

    [SerializeField] private GameObject keyIcon;


    private bool hasKey;
    private int coinAmount;
    private int health;

    private void OnEnable()
    {
        if(PlayerData.Instance.hasKey)
        {
            keyIcon.SetActive(true);
            hasKey = true;
        }
        if(PlayerData.Instance.currentHealth == 0) { health = 5; } 
        else { health = PlayerData.Instance.currentHealth; }
        
        coinAmount = Coin.coinValue;
        

        coinInput.text = coinAmount.ToString();
        healthInput.text = health.ToString();

    }


    public void setKey()
    {
        hasKey = !hasKey;
    }


    public void setCoin()
    {
        coinAmount = int.Parse(coinInput.text);
    }

    public void setHealth()
    {
        health = int.Parse(healthInput.text);
    }


    public void reloadData()
    {
        PlayerData.Instance.hasKey = hasKey;
        
        
        Coin.coinValue += coinAmount;
        PlayerData.Instance.currentHealth = health;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
