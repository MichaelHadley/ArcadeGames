using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    //string FilePath = "Assets/Resources/SpaceInvadersHighScore.txt";

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    [Header("Prefabs")]
    public GameObject GameOverCanvas;
    public GameObject playerPrefab;
    public GameObject shield;

    [Header("Transforms")]
    public Transform spawnPoint;
    public Transform shieldSpawn;
    public Transform shieldParent;

    [Header("Score")]
    public TextMeshProUGUI scoreText, highScoreText;
    public int score;

    [Header("Lives")]
    public GameObject Life1, Life2, Life3;

    [Header("Life count")]
    public int numOfLives = 3;

    private GameObject[] bosses;

    // Delay death
    private float stopProgress;
    private bool stopped;

    private void Start()
    {
        SpawnShields();

        // Display "HighScore"
        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        PauseWhenDead();
    }

    private void PauseWhenDead()
    {
        // Stopped is player dead, pause game for 2 seconds then restart
        if (stopped)
        {
            stopProgress += Time.unscaledDeltaTime / 1.3f;
            if (stopProgress >= 1)
            {
                stopProgress = 0;
                stopped = false;
                Time.timeScale = 1;
            }
        }
    }

    private void SpawnShields()
    {
        Destroy(GameObject.FindWithTag("Shield"));

        GameObject shields;

        shields = Instantiate(shield, shieldSpawn.position + new Vector3(-15, -9, 0), Quaternion.identity);
        shields = Instantiate(shield, shieldSpawn.position + new Vector3(-5, -9, 0), Quaternion.identity);
        shields = Instantiate(shield, shieldSpawn.position + new Vector3(5, -9, 0), Quaternion.identity);
        shields = Instantiate(shield, shieldSpawn.position + new Vector3(15, -9, 0), Quaternion.identity);

        shields.transform.SetParent(shieldParent);
    }

    public void ScoreFunction(string reason)
    {
        if (reason == "Enemy10")
        {
            score += 10;
            scoreText.text = score + " ";
        }
        else if (reason == "Enemy20")
        {
            score += 20;
            scoreText.text = score + " ";
        }
        else if (reason == "Enemy30")
        {
            score += 30;
            scoreText.text = score + " ";
        }
        else if (reason == "Boss100")
        {
            score += 100;
            scoreText.text = score + " ";
        }

        if (EnemyManager.Instance.numOfEnemies == 0)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        // Reset game time
        Time.timeScale = 1f;
        
        // Reset enemies
        EnemyManager.Instance.SpawnEnemy();
        EnemyManager.Instance.numOfEnemies = 50;
        
        // Reset shield defences
        SpawnShields();

        // Reset player
        SpawnPlayer();
    }

    public void PlayerDeath()
    {
        numOfLives--;
        Debug.Log("life lost");

        // If life lost reduce num of lifes and spawn player
        // If num of lives is equal to 0 the game is over
        if (numOfLives == 0)
        {
            UpdateHighScore();

            GameOver();
        }
        else
        {
            StartCoroutine(SpawnPlayer());
            stopped = true;
            Time.timeScale = 0f;
        }

        // Turn off life game object in game UI if life is lost
        if (numOfLives == 2)
        {
            Life3.SetActive(false);
        }
        else if (numOfLives == 1)
        {
            Life2.SetActive(false);
        }
        else if (numOfLives == 0)
        {
            Life1.SetActive(false);
        }
    }

    private void UpdateHighScore()
    {
        // If the current "HighScore" is less than the scored value set new highscore
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            // Set "HighScore" from score value
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void GameOver()
    {
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject boss in bosses)
        {
            boss.GetComponent<Boss>().bossMovementClip.Pause();
        }

        // Dispay GameOverCanvas once the player has died 3 times
        GameOverCanvas.SetActive(true);
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider col)
    {
        // If hit by enemy bullet set player position equal to the spawn point position
        if (col.transform.tag == "EnemyBullet")
        {
            transform.position = spawnPoint.position;
        }
    }
}
