using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameMenu : MonoBehaviour
{ 
    //Play UI in FroggerMenu loads frogger scene when pressed
    public void PlayFrogger()
    {
        SceneManager.LoadScene("Frogger");
        Debug.Log("Frogger Loaded");
    }

    //Frogger UI in Main menu
    public void FroggerMenu()
    {
        SceneManager.LoadScene("FroggerMenu");
        Debug.Log("Frogger Menu Loaded");
    }

    public void ResetFroggerHighScore()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/FroggerHighScore.txt", false);
        writer.WriteLine("0");
        writer.Close();
    }

    public void QuitFrogger()
    {
        SceneManager.LoadScene("GameMenu");
        Debug.Log("Game Menu Loaded");
    }

    public void PlaySpaceInvaders()
    {
        SceneManager.LoadScene("SpaceInvaders");
        Debug.Log("SpaceInvaders Loaded");
    }

    public void SpaceInvadersMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
        Debug.Log("SpaceInvaders Menu Loaded");
    }

    public void QuitSpaceInvaders()
    {
        SceneManager.LoadScene("GameMenu");
        Debug.Log("Game Menu Loaded");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

}
