using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip unpauseSound;
    [Space(15)]
    [SerializeField] private Texture2D customCursor;
    private Vector2 cursorOffset;

    private AudioSource soundSource;
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        // Crosshair
        cursorOffset = new Vector2(customCursor.width / 2, customCursor.height / 2);
        Cursor.SetCursor(customCursor, cursorOffset, CursorMode.Auto);
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
        /*Time.fixedDeltaTime = 0.02f;*/
        GameIsPaused = false;

        AudioListener.pause = false;

        soundSource.PlayOneShot(unpauseSound);
    }

    void Pause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        /*Time.fixedDeltaTime = 0f;*/
        GameIsPaused = true;
        soundSource.PlayOneShot(pauseSound);

        AudioListener.pause = true;

    }
}
