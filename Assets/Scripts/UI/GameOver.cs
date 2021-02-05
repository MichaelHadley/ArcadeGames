using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }
}
