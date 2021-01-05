using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    //Frogger UI in Main menu
    public void FroggerMenu()
    {
        SceneManager.LoadScene("FroggerMenu");
    }

    //Play UI in FroggerMenu loads frogger scene when pressed
    public void PlayFrogger()
    {
        SceneManager.LoadScene("FroggerMenu");
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
