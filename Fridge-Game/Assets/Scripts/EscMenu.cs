using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public Rigidbody rb;

    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Paused();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Paused()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.1f;
        GameIsPaused = true;
    }
}
