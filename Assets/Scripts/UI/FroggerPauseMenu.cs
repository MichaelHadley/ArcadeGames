﻿using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FroggerPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource froggerMusic;

    private bool GameIsPaused = false;
    private bool pauseButton;
    private bool buttonReleased = true;

    void Update()
    {
        //Pause menu keypress code here
        pauseButton = Input.GetKey(KeyCode.Escape);

        if (pauseButton)
        {
            buttonReleased = false;

            // If game is not paused then pause the game otherwise unpause the game
            if (!GameIsPaused)
            {
                Time.timeScale = 0;
                froggerMusic.Play();
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                froggerMusic.Pause();
                pauseMenu.SetActive(false);
            }
        }
        else
        {
            // Need to use button released before updating game is paused so that holding key down for a long time only classes as one press
            if (!buttonReleased)
            {
                buttonReleased = true;
                GameIsPaused = !GameIsPaused;
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        froggerMusic.Pause();
        pauseMenu.SetActive(false);
    }

    public void QuitFrogger()
    {
        GameIsPaused = false;
        buttonReleased = true;
        ResumeGame();
        DOTween.KillAll();
        froggerMusic.UnPause();
        SceneManager.LoadScene("FroggerMenu");
    }
}
