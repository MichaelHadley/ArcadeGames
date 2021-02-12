using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    string FilePath = "Assets/Resources/SpaceInvadersHighScore.txt";

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

    public GameObject GameOverCanvas;
    public Transform spawnPoint;
    public int numOfLives = 3;
    public GameObject playerPrefab;
    public GameObject shield;
    public Transform shieldSpawn;
    public Transform shieldParent;

    // Score
    public TextMeshProUGUI scoreText, highScoreText;
    public int score;
    public int highScore;
    public GameObject Life1, Life2, Life3;


    private void Start()
    {
        SpawnShields();

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

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
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
            if (score >= highScore)
            {
                StreamWriter writer = new StreamWriter(FilePath, false);
                writer.WriteLine(score.ToString());
                writer.Close();
            }
            GameOver();
        }
        else
        {
            StartCoroutine(SpawnPlayer());
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

    public IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2);
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void GameOver()
    {
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
