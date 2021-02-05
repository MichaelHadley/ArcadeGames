using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FroggerManager : MonoBehaviour
{
    public GameObject LevelCompleteCanvas;

    public GameObject lifeOne;
    public GameObject lifeTwo;
    public GameObject lifeThree;

    public TextMeshProUGUI score;
   // public GameObject Score;

    public GameObject GameOverCanvas;
    public GameObject Player;
    private float stopProgress;
    private bool stopped;

    private static FroggerManager _instance;
    public static FroggerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FroggerManager>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    private int lives = 3;
    public int points = 0;
    private int frogAtEnd = 0;


    public void Update()
    {
        if (stopped)
        {
            stopProgress += Time.unscaledDeltaTime;
            if(stopProgress >= 1)
            {
                stopProgress = 0;
                stopped = false;
                Time.timeScale = 1;
                Player.transform.parent = null;
                Player.transform.position = GetComponent<Frogger>().playerSpawn.position;
                GetComponent<Frogger>().playerLane = 0;
                GetComponent<Frogger>().maxPlayerLane = 0;
                Player.transform.DOKill();
            }
        }
    }

    public void AwardPoints(string reason)
    {
        if(reason == "laneProgress")
        {
            points += 10;
            score.text = points + " ";
        }
        else if(reason == "Goal")
        {
            points += 100;
            score.text = points + " ";
        }
    }

    public void Death()
    {
        //change lives by one and if now at zero lives then game over
        //move player back to start
        lives--;
        Debug.Log("Life lost");

        if(lives == 0)
        {
            EndGame();
        }
        else
        {
            stopped = true;
            Time.timeScale = 0;
        }

        if (lives == 2)
        {
            lifeThree.SetActive(false);
        }
        else if(lives == 1)
        {
            lifeTwo.SetActive(false);
        }
        else if (lives == 0)
        {
            lifeOne.SetActive(false);
        }

    }
  
    public void Goal()
    {
        //frog reaches end goal award points and reset play pos to start or game win if all frogs have reached end point
        //score points for how long it takes to get across
        frogAtEnd++;
        Debug.Log("Goal");

        AwardPoints("Goal");
        if (frogAtEnd == 4)
        {
            gameHasEnded = true;
            Debug.Log("Level Won");

            //activate canvas on screen for the player to proceed to next level or quit game
            LevelCompleteCanvas.SetActive(true);

            //Stops everything in the scene
            DOTween.KillAll();
            Time.timeScale = 0f;
        }
        else
        {
            Player.transform.parent = null;
            Player.transform.position = GetComponent<Frogger>().playerSpawn.position;
            GetComponent<Frogger>().playerLane = 0;
            GetComponent<Frogger>().maxPlayerLane = 0;
            Player.transform.DOKill();
        }
    }

    bool gameHasEnded = false;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;

            GameOverCanvas.SetActive(true);

            Debug.Log("GameOver");
            
            DOTween.KillAll();
            Time.timeScale = 0f;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        lives = 3;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        //Cursor.visible = false;
        GameOverCanvas.SetActive(false);
        Restart();
    }

    public void QuitFrogger()
    {
        PlayAgain();
        SceneManager.LoadScene("FroggerMenu");
    }
}
