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
        SceneManager.LoadScene("Frogger");
    }

    public void LoadSpaceInvadersMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }

    //Quits frogger and returns player to start menu
    public void QuitFrogger()
    {
        SceneManager.LoadScene("GameMenu");
    }

    //Close down game
    public void QuitGame()
    {
        Application.Quit();
    }

}
