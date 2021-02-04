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

    // Update is called once per frame
    void Update()
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

        // Increase movement speed of enemies by multiplier is numOfEnemies is less than the value
        if (numOfEnemies < 2)
        {
            speedMultiplier = 10f;
        }
        else if (numOfEnemies < 5)
        {
            speedMultiplier = 5f;
        }
        else if (numOfEnemies < 10)
        {
            speedMultiplier = 2f;
        }
        else if (numOfEnemies < 20)
        {
            speedMultiplier = 1.5f;
        }
    }
}
