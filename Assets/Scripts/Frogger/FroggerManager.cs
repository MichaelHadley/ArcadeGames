using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;


public class FroggerManager : MonoBehaviour
{
    // Class instance
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

    [Header("GameObjects")]
    public GameObject Player;
    public GameObject lifeOne, lifeTwo, lifeThree;
    public GameObject goalOne, goalTwo, goalThree, goalFour;

    [Header("UI Text")]
    public TextMeshProUGUI scoreText, highScoreText, GameOverScore;

    [Header("UI Objects")]
    public GameObject LevelCompleteCanvas;
    public GameObject GameOverCanvas;

    [Header("Audio")]
    public AudioClip gameOverMusic;

    [Header("Score Variables")]
    private int Scored;
    
    private int lives = 3;
    private int frogAtEnd = 0;

    // Delay death
    private float stopProgress;
    private bool stopped;

    private void Start()
    {
        // Display "highscore"
        highScoreText.text = "" + PlayerPrefs.GetInt("highscore");
    }

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
            Scored += 10;
            scoreText.text = Scored + " ";
            GameOverScore.text = Scored + " ";
        }
        else if(reason == "Goal")
        {
            Scored += 100;
            scoreText.text = Scored + " ";
            GameOverScore.text = Scored + " ";
        }
    }

    public void UpdateHighScore()
    {
        // If the current "highscore" is less than the scored value set new highscore
        if (PlayerPrefs.GetInt("highscore") < Scored)
        {
            // Set "highscore" from scored value
            PlayerPrefs.SetInt("highscore", Scored);
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
            UpdateHighScore();
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
            
            Debug.Log("Level Won");

            //activate canvas on screen for the player to proceed to next level or quit game
            LevelCompleteCanvas.SetActive(true);

            //Stops everything in the scene
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

            gameObject.GetComponent<AudioSource>().PlayOneShot(gameOverMusic);
            gameObject.GetComponent<AudioSource>().loop = true;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);       
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        LevelCompleteCanvas.SetActive(false);
        //reset goals
        goalOne.transform.GetChild(2).gameObject.SetActive(false);
        goalTwo.transform.GetChild(2).gameObject.SetActive(false);
        goalThree.transform.GetChild(2).gameObject.SetActive(false);
        goalFour.transform.GetChild(2).gameObject.SetActive(false);
        frogAtEnd = 0;
        //reset player
        Player.transform.parent = null;
        Player.transform.position = GetComponent<Frogger>().playerSpawn.position;
        GetComponent<Frogger>().playerLane = 0;
        GetComponent<Frogger>().maxPlayerLane = 0;
        Player.transform.DOKill();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        //Cursor.visible = false;
        GameOverCanvas.SetActive(false);
        gameObject.GetComponent<AudioSource>().Stop();
        Restart();
    }

    public void QuitFrogger()
    {
        PlayAgain();
        gameObject.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("FroggerMenu");
    }
}
