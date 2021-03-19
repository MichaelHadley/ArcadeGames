using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene("SpaceInvaders");
        // Reset game time
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
        // Reset game time
        Time.timeScale = 1f;
    }
}
