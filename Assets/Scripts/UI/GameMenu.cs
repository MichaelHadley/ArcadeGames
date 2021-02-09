using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    public void ResetFroggerHighScore()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/FroggerHighScore.txt", false);
        writer.WriteLine("0");
        writer.Close();
    }

    public void QuitFrogger()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void PlaySpaceInvaders()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void SpaceInvadersMenu()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }

    public void QuitSpaceInvaders()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
