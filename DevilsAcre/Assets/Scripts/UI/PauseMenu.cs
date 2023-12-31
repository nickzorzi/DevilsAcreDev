using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public PlayerController playerController;
    public UIManager uiManager;
    public ShopMenu shopMenu;

    public GameObject pauseMenuUI;
    [SerializeField] private GameObject CheatUI;
    public static bool CheaterMode;


    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip unpauseSound;
    [Space(15)]
    [SerializeField] private Texture2D customCursor;
    private Vector2 cursorOffset;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        shopMenu = FindObjectOfType<ShopMenu>();
        uiManager = FindObjectOfType<UIManager>();
        
        // Crosshair
        cursorOffset = new Vector2(customCursor.width / 2, customCursor.height / 2);
        Cursor.SetCursor(customCursor, cursorOffset, CursorMode.Auto);
    }

    void Update()
    {
        if (!uiManager.uiOpen && !ShopMenu.ShopIsOpen && !playerController.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }


    private void OnDisable()
    {
        if (GameIsPaused) { Resume(); }
    }

    public void Resume()
    {
        Cursor.SetCursor(customCursor, cursorOffset, CursorMode.Auto);

        if (CheaterMode)
        {
            CheatUI.SetActive(false);
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        /*Time.fixedDeltaTime = 0.02f;*/
        GameIsPaused = false;

        AudioListener.pause = false;

        SoundManager.Instance.PlaySound(unpauseSound);

        playerController.noShooting = false;
    }

    public void Pause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        if(CheaterMode)
        {
            CheatUI.SetActive(true);
        }

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        /*Time.fixedDeltaTime = 0f;*/
        GameIsPaused = true;
        SoundManager.Instance.PlaySound(pauseSound);

        AudioListener.pause = true;

        playerController.noShooting = true;
    }

    public void turnOnCheats()
    {
        CheaterMode = true;
    }
}
