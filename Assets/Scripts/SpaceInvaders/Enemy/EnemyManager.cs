using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool reachedBarrier;
    public bool moveDown;
    public int direction = 1;

    public int numOfEnemies = 50;
    public float speedMultiplier = 1f;

    public float spawnTimer = 10f;

    [Header("Enemy Prefabs")]
    public GameObject enemyRowOne;
    public GameObject enemyRowTwo;
    public GameObject enemyRowThree;
    public GameObject enemyRowFour;
    public GameObject enemyRowFive;
    public GameObject boss;

    [Header("Enemy Transforms")]
    public Transform enemySpawnPoint;
    public Transform rowOne;
    public Transform rowTwo;
    public Transform rowThree;
    public Transform rowFour;
    public Transform rowFive;

    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyManager>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    private void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        IncreaseMovement();
    }

    public void SpawnBoss()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
        }
    }

    void EnemyDeath()
    {
        if (numOfEnemies == 0)
        {
            SpawnEnemy();
        }
    }

    
    public void SpawnEnemy()
    {
        GameObject enemies;

        for (int i = 0; i < 10; i++)
        {
            enemies = Instantiate(enemyRowOne, enemySpawnPoint.position + new Vector3(2 * i, 0, 0), Quaternion.identity);
            enemies.transform.SetParent(rowOne);

            enemies = Instantiate(enemyRowTwo, enemySpawnPoint.position + new Vector3(2 * i, -2, 0), Quaternion.identity);
            enemies.transform.SetParent(rowTwo);

            enemies = Instantiate(enemyRowThree, enemySpawnPoint.position + new Vector3(2 * i, -4, 0), Quaternion.identity);
            enemies.transform.SetParent(rowThree);

            enemies = Instantiate(enemyRowFour, enemySpawnPoint.position + new Vector3(2 * i, -6, 0), Quaternion.identity);
            enemies.transform.SetParent(rowFour);

            enemies = Instantiate(enemyRowFive, enemySpawnPoint.position + new Vector3(2 * i, -8, 0), Quaternion.identity);
            enemies.transform.SetParent(rowFive);
        }
    }

    private void ChangeDirection()
    {
        if (reachedBarrier)
        {
            reachedBarrier = false;

            // Move enemies down when barrier is reached
            moveDown = true;

            // Reverse direction
            direction = -direction;
        }
        else
        {
            moveDown = false;
        }
    }
     
    private void IncreaseMovement()
    {
        // Increase movement speed of enemies by multiplier is numOfEnemies is less than the value
        if (numOfEnemies < 2)
        {
            speedMultiplier = 10.0f;
        }
        else if (numOfEnemies < 5)
        {
            speedMultiplier = 7.5f;
        }
        else if (numOfEnemies < 10)
        {
            speedMultiplier = 5.0f;
        }
        else if (numOfEnemies < 20)
        {
            speedMultiplier = 2.5f;
        }
        else if (numOfEnemies < 30)
        {
            speedMultiplier = 2.0f;
        }
        else if (numOfEnemies < 40)
        {
            speedMultiplier = 1.5f;
        }
        else
        {
            speedMultiplier = 1f;
        }
    }
}