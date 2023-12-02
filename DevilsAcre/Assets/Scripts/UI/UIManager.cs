using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{


    public GameObject gameOverMenu;
    public GameObject levelUpMenu;
    public GameObject victoryMenu;

    public GameObject shoot1xUI;
    public GameObject shoot2xUI;
    public GameObject shoot125xUI;
    public GameObject dashUI;
    public GameObject axeUI;
    public GameObject molotovUI;

    public Projectile projectile;
    public PlayerController playerController;

    private Score scoreManager;
    public Coin coinManager;

    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource levelUpSound;
    [SerializeField] private AudioSource gameOverSound;

    private void Start()
    {
        scoreManager = FindObjectOfType<Score>();
        coinManager = FindObjectOfType<Coin>();
        playerController = FindObjectOfType<PlayerController>();
        projectile = FindObjectOfType<Projectile>();

        victorySound.ignoreListenerPause = true;
        levelUpSound.ignoreListenerPause = true;
        gameOverSound.ignoreListenerPause = true;

        if(PlayerData.Instance.canDash)
        {
            CurseRapidSprint();
        }
        if(PlayerData.Instance.canDoubleEdged)
        {
            CurseDoubleEdged();
        }
        if(PlayerData.Instance.canQuickfire)
        {
            CurseQuickfire();
        }
        if(PlayerData.Instance.canAxe)
        {
            PlayerAxe();
        }
        if(PlayerData.Instance.canMolotov)
        {
            PlayerMolotov();
        }
    }
    
    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += EnableGameOverMenu;
        PlayerController.OnLevelUp += EnableLevelUpMenu;
        PlayerController.OnVictory += EnableVictoryMenu;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= EnableGameOverMenu;
        PlayerController.OnLevelUp -= EnableLevelUpMenu;
        PlayerController.OnVictory -= EnableVictoryMenu;
    }

    public void EnableGameOverMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameOverMenu.SetActive(true);
        AudioListener.pause = true;

        gameOverSound.Play();
    }

    public void EnableLevelUpMenu()
    {
        levelUpMenu.SetActive(true);
        AudioListener.pause = true;

        levelUpSound.Play();
    }

    public void DisableLevelUpMenu()
    {
        levelUpMenu.SetActive(false);
        Debug.Log("Disabled Level Up Menu");
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void EnableVictoryMenu()
    {
        victoryMenu.SetActive(true);
        AudioListener.pause = true;

        victorySound.Play();   
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Tutorial");
        Score.scoreValue = 0;
        Coin.coinValue = 0;

        PlayerData.Instance.lastWave = 0;
        PlayerData.Instance.hasKey = false;
        PlayerData.Instance.ResetBools();

        Projectile.damage = 1;
        playerController.damage = 1;
        playerController.speed = 5f;
        playerController.timeBetweenShots = 1f;

        playerController.dashSpeed = 0;
        playerController.dashCooldown = 0;
        playerController.dashDuration = 0f;

        PauseMenu.GameIsPaused = false;

        PlayerController.allowLevelMenu = true;
        PlayerController.allowVictoryMenu = true;

        playerController.canDash = false;

        AudioListener.pause = false;

        shoot1xUI.SetActive(true);
        shoot2xUI.SetActive(false);
        shoot125xUI.SetActive(false);
        dashUI.SetActive(false);
        axeUI.SetActive(false);
        molotovUI.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        AudioListener.pause = false;

        PlayerData.Instance.lastWave = 0;
        PlayerData.Instance.hasKey = false;
        PlayerData.Instance.ResetBools();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void CurseDoubleEdged()
    {
        // Debug.Log("projectile =" + projectile);
        // Debug.Log("playerController =" + playerController);
        // if (projectile != null)
        // {
        //     projectile.damage = 2;
        // }
        Projectile.damage = 2;

        if (playerController != null)
        {
            playerController.damage = 2;

            DisableLevelUpMenu();
            Debug.Log("Curse of Double Edged Selected");

            PlayerData.Instance.canDoubleEdged = true;

            shoot1xUI.SetActive(true);
            shoot2xUI.SetActive(false);
            shoot125xUI.SetActive(false);
            dashUI.SetActive(false);

            AudioListener.pause = false;
        }
    }

    public void CurseQuickfire()
    {
        if (playerController != null)
        {
            playerController.timeBetweenShots = 0.5f;
            playerController.speed = 2.5f;

            DisableLevelUpMenu();
            Debug.Log("Curse of Quickfire Selected");

            PlayerData.Instance.canQuickfire = true;

            shoot1xUI.SetActive(false);
            shoot2xUI.SetActive(true);
            shoot125xUI.SetActive(false);
            dashUI.SetActive(false);

            AudioListener.pause = false;
        }
    }

    public void CurseRapidSprint()
    {
        if (playerController != null)
        {
            playerController.timeBetweenShots = 0.75f;
            playerController.speed = 2.5f;
            playerController.dashSpeed = 20;
            playerController.dashCooldown = 1;
            playerController.dashDuration = 0.2f;

            DisableLevelUpMenu();
            Debug.Log("Curse of Rapid Sprint Selected");

            playerController.canDash = true;
            PlayerData.Instance.canDash = true;

            shoot1xUI.SetActive(false);
            shoot2xUI.SetActive(false);
            shoot125xUI.SetActive(true);
            dashUI.SetActive(true);

            AudioListener.pause = false;
        }
    }

    public void PlayerAxe()
    {
        if (Coin.coinValue >= 14 && !playerController.canAxe)
        {
            if (playerController != null)
            {
                axeUI.SetActive(true);
                playerController.canAxe = true;
                PlayerData.Instance.canAxe = true;
                Coin.coinValue -= 14;
                Debug.Log("Bought Axe");
            }
        }
        else if(PlayerData.Instance.canAxe)
        {
            axeUI.SetActive(true);
            playerController.canAxe = true;
            Debug.Log("Data Successfully Transfered");
            return;
        }
            Debug.Log("Not Enough Money");
    }

    public void PlayerMolotov()
    {
        if (Coin.coinValue >= 14 && !playerController.canMolotov)
        {
            if (playerController != null)
            {
                molotovUI.SetActive(true);
                playerController.canMolotov = true;
                PlayerData.Instance.canMolotov = true;
                Coin.coinValue -= 14;
                Debug.Log("Bought Molotov");
            }
        }
        else if(PlayerData.Instance.canMolotov)
        {
            molotovUI.SetActive(true);
            playerController.canMolotov = true;
            Debug.Log("Data Successfully Transfered");
            return;
        }
            Debug.Log("Not Enough Money");
    }

    public void PlayerHeal()
    {
        if (Coin.coinValue >= 3 && playerController.currentHealth < playerController.maxHealth)
        {
            if (playerController != null)
            {
                playerController.currentHealth ++;
                playerController.healthBar.SetHealth(playerController.currentHealth);

                Coin.coinValue -= 3;
                Debug.Log("Bought Heal");
            }
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

}
