using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public Projectile projectile;
   public PlayerController playerController;
   public UIManager uiManager;

   private Score scoreManager;

   public void PlayGame()
   {
      SceneManager.LoadScene(1);
      Score.scoreValue = 0;

      Projectile.damage = 1;
      playerController.damage = 1;
      playerController.speed = 5f;
      playerController.timeBetweenShots = 1f;

      PauseMenu.GameIsPaused = false;

      PlayerController.allowLevelMenu = true;
      PlayerController.allowVictoryMenu = true;

      playerController.canDash = false;

      uiManager.shoot1xUI.SetActive(true);
      uiManager.shoot2xUI.SetActive(false);
      uiManager.shoot125xUI.SetActive(false);
      uiManager.dashUI.SetActive(false);

      AudioListener.pause = false;
   }

   public void ExitCredits()
   {
      SceneManager.LoadScene(0);
   }
}
