using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    [SerializeField] private AudioSource pauseSound;
    [SerializeField] private AudioSource unpauseSound;
    [Space(15)]
    [SerializeField] private Texture2D customCursor;
    private Vector2 cursorOffset;
    void Start()
    {
        // Crosshair
        cursorOffset = new Vector2(customCursor.width / 2, customCursor.height / 2);
        Cursor.SetCursor(customCursor, cursorOffset, CursorMode.Auto);
        pauseSound.ignoreListenerPause = true;
        unpauseSound.ignoreListenerPause = true;
    }

    void Update()
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

    public void Resume()
    {
        Cursor.SetCursor(customCursor, cursorOffset, CursorMode.Auto);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        AudioListener.pause = false;

        unpauseSound.Play();
    }

    void Pause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        AudioListener.pause = true;

        pauseSound.Play();
    }
}
