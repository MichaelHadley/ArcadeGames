using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{ 
    //Play UI in FroggerMenu loads frogger scene when pressed
    public void PlayFrogger()
    {
        SceneManager.LoadScene("Frogger");
    }

    //Frogger UI in Main menu
    public void FroggerMenu()
    {
        SceneManager.LoadScene("FroggerMenu");
    }


    public void QuitFrogger()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void PlaySpaceInvaders()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void LoadSpaceInvadersMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }

<<<<<<< HEAD
    //Quits frogger and returns player to start menu
    public void QuitFrogger()
=======
    public void QuitSpaceInvaders()
>>>>>>> 90c8c377ac83838bf5897b93ff9ccddae28211f4
    {
        SceneManager.LoadScene("GameMenu");
    }

<<<<<<< HEAD
    //Close down game
=======
>>>>>>> 90c8c377ac83838bf5897b93ff9ccddae28211f4
    public void QuitGame()
    {
        Application.Quit();
    }

}
