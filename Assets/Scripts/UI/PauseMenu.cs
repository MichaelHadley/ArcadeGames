using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false;

    public GameObject pauseMenu;

    private bool pauseButton;

    private bool buttonReleased = true;

    void Update()
    {
        //Pause menu keypress code here
        pauseButton = Input.GetKey(KeyCode.Escape);

        if (pauseButton)
        {
            buttonReleased = false;

            // If game is not paused then pause the game otherwise unpause the game
            if (!GameIsPaused)
            {
                Time.timeScale = 0;
                //Cursor.visible = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                //Cursor.visible = false;
                pauseMenu.SetActive(false);
            }
        }
        else
        {
            // Need to use button released before updating game is paused so that holding key down for a long time only classes as one press
            if (!buttonReleased)
            {
                buttonReleased = true;
                GameIsPaused = !GameIsPaused;
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void QuitSpaceInvaders()
    {
        GameIsPaused = false;
        buttonReleased = true;
        ResumeGame();
        SceneManager.LoadScene("SpaceInvadersMenu");
    }
    public void QuitFrogger()
    {
        GameIsPaused = false;
        buttonReleased = true;
        ResumeGame();
        SceneManager.LoadScene("FroggerMenu");
    }
}
