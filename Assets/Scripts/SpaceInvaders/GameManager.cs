using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    // Score
    public TextMeshProUGUI scoreText;
    public int score;

    public GameObject Life1, Life2, Life3;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = "Score " + score.ToString();
    }

    public void ScoreFunction(string reason)
    {
        if (reason == "Enemy10")
        {
            score += 10;
        }
        else if (reason == "Enemy20")
        {
            score += 20;
        }
        else if (reason == "Enemy30")
        {
            score += 30;
        }
        else if (reason == "BossKilled")
        {
            score += 100;
        }
    }

    public void PlayerDeath()
    {
        numOfLives--;

        if (numOfLives == 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(SpawnPlayer());
        }
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

    IEnumerator SpawnPlayer()
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
        if (col.transform.tag == "EnemyBullet")
        {
            transform.position = spawnPoint.position;
        }
    }
}
