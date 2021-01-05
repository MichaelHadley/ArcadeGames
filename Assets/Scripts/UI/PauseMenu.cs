﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;

    void Update()
    {

        if (InputManager.pause)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
        }
        if (InputManager.unpause)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            pauseMenu.SetActive(false);

        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }
    public void QuidGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}