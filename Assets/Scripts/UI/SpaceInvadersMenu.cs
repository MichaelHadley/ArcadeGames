using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceInvadersMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void Settings()
    {
        // Open scene UI
    }

    public void HighScore()
    {
        // Open scene UI
    }

    public void OnApplicationQuit()
    {
        SceneManager.LoadScene("GameMenu");
    }

}
