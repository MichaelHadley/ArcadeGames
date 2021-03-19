using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoadNewGame()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("SpaceInvaders");
        // Reset game time
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("SpaceInvadersMenu");
        // Reset game time
        Time.timeScale = 1f;
    }
}