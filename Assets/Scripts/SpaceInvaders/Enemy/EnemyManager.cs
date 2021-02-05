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
        ChangeDirection();
        MovementIncrease();
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
     
    private void MovementIncrease()
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
    }
}