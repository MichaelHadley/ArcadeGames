using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;

    void Update()
    {

        if (InputManager.pause)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
        }
        if (InputManager.unpause)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            pauseMenu.SetActive(false);

        }
    }

<<<<<<< HEAD
    public void MainMenu()
    {
        SceneManager.LoadScene("FroggerMenu");
    }

    public void UnpauseGame()
=======
    public void ResumeGame()
>>>>>>> 90c8c377ac83838bf5897b93ff9ccddae28211f4
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void QuitSpaceInvaders()
    {
        SceneManager.LoadScene("SpaceInvadersMenu");
    }
    public void QuitFrogger()
    {
<<<<<<< HEAD
        SceneManager.LoadScene("GameMenu");
=======
        SceneManager.LoadScene("FroggerMenu");
>>>>>>> 90c8c377ac83838bf5897b93ff9ccddae28211f4
    }
}
