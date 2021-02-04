using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        player, enemy
    }

    public BulletType bulletType;

    public float bulletSpeed = 5f;

    public Sprite explodedShipImage;

    private static Bullet _instance;
    public static Bullet Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Bullet>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }


    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        // If bullet type is player increase the velocity for the bullet to travel up the Y axis
        if (bulletType == BulletType.player)
        {
            pos += velocity;
        }
        // Otherwise the bullet type is enemy so the bullet will travel down the y axis
        else
        {
            pos -= velocity;
        }

        transform.position = pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && bulletType == BulletType.player)
        {
            // Play explosion sound effect when hit
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.enemyDies);

            // Initiate explosion spite
            collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            // Mark enemy as dead to stop it updating while exploded ship image is shown
            collision.GetComponent<EnemyController>().isDead = true;

            EnemyManager.Instance.numOfEnemies--;

            // Destroy explosion after 0.5 of a second
            Destroy(collision.gameObject, 0.5f);

            // Destroy bullet
            Destroy(gameObject);

            // Call function to increase score when enemy is destroyed
            //IncreaseTextUIScore();
        }
        else if (collision.gameObject.tag == "Player" && bulletType == BulletType.enemy)
        {
            // Play explosion sound effect when hit
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

            // Initiate explosion spite
            collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            // Destroy bullet
            Destroy(gameObject);

            // Destroy explosion after 0.5 of a second
            Destroy(collision.gameObject, 0.5f);

            // Destroy player
            Destroy(collision.gameObject, 0.5f);
        }
        // Destroy bullet and shield when collision occurs
        else if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        // Destroy bullet if it collides with the world barrier
        else if (collision.gameObject.tag == "WorldBarrier")
        {
            Destroy(gameObject);
        }
    }

    //void IncreaseTextUIScore()
    //{
    //    var textUI = GameObject.Find("Score").GetComponent<Text>();

    //    int score = int.Parse(textUI.text);

    //    score += 10;

    //    textUI.text = score.ToString();
    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}