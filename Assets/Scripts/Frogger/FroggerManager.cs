using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class FroggerManager : MonoBehaviour
{
    public GameObject LevelCompleteCanvas;

    public GameObject lifeOne, lifeTwo, lifeThree;

    public GameObject goalOne, goalTwo, goalThree, goalFour;


    public TextMeshProUGUI score, highScoreText, GameOverScore;


    public GameObject GameOverCanvas;
    public GameObject Player;
    private float stopProgress;
    private bool stopped;

    string FilePath = "Assets/Resources/FroggerHighScore.txt";

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
    public int Scored;
    public int highScore;
    private int frogAtEnd = 0;

    private void Start()
    {
        if (File.Exists(FilePath))
        {
            //Write some text to the test.txt file
            StreamReader reader = new StreamReader(FilePath);
            string tmp = reader.ReadLine();
            highScore = int.Parse(tmp);
            reader.Close();
        }
        else 
        {
            highScore = 0;
        }
        highScoreText.text = highScore.ToString();
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
            score.text = Scored + " ";
            GameOverScore.text = Scored + " ";
        }
        else if(reason == "Goal")
        {
            Scored += 100;
            score.text = Scored + " ";
            GameOverScore.text = Scored + " ";
        }
    }

    public void UpdateHighScore()
    {
        if (Scored > highScore)
        {
            highScore = Scored;
        }
        highScoreText.text = highScore.ToString();
    }

    public void Death()
    {
        //change lives by one and if now at zero lives then game over
        //move player back to start
        lives--;
        Debug.Log("Life lost");

        if(lives == 0)
        {
            //Write some text to the test.txt file
            if(Scored >= highScore)
            {
                StreamWriter writer = new StreamWriter(FilePath, false);
                writer.WriteLine(Scored.ToString());
                writer.Close();
            }
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
        Restart();
    }

    public void QuitFrogger()
    {
        PlayAgain();
        SceneManager.LoadScene("FroggerMenu");
    }
}
