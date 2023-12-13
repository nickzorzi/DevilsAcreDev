using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



   public void PlayGame()
   {
      SceneManager.LoadScene("Tutorial");
        Score.scoreValue = 0;
        Coin.coinValue = 0;

        PlayerData.Instance.lastWave = 0;
        PlayerData.Instance.hasKey = false;
        PlayerData.Instance.ResetBools();

        Projectile.damage = 1;

        PauseMenu.GameIsPaused = false;

        PlayerController.allowLevelMenu = true;
        PlayerController.allowVictoryMenu = true;

        Time.timeScale = 1;

        AudioListener.pause = false;

   }

    public void ExitCredits()
   {
      SceneManager.LoadScene(0);
   }

   public void GoToCredits()
    {
        SceneManager.LoadScene(2);
    }
}
