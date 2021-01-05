using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void PlayFrogger()
    {
        SceneManager.LoadScene("Frogger");
    }

    public void LoadSpaceInvadersMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
