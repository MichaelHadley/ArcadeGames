﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        player, enemy
    }

    public BulletType bulletType;

    public float bulletSpeed = 5f;

    public Sprite explodedShipImage;

    [Header("Audio")]
    public AudioClip enemyDiesClip;
    public AudioClip shipExplosion;

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
        // Bullet collision with enemy
        if (collision.gameObject.tag == "Enemy" && bulletType == BulletType.player)
        {
            if (collision.GetComponent<EnemyController>().isDead == false)
            {
                // Play audioclip
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(enemyDiesClip, 1f);

                // Initiate explosion spite
                collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

                // Mark enemy as dead to stop it updating while exploded ship image is shown
                collision.GetComponent<EnemyController>().isDead = true;

                EnemyManager.Instance.numOfEnemies--;

                string enemyKilled = "Enemy" + collision.GetComponent<EnemyController>().scoreValue.ToString();

                GameManager.Instance.ScoreFunction(enemyKilled);

                // Destroy explosion after 0.5 of a second
                Destroy(collision.gameObject, .5f);

                // Destroy bullet
                Destroy(gameObject);
            }
        }
        // Bullet collision with boss
        else if (collision.gameObject.tag == "Boss" && bulletType == BulletType.player)
        {
            if (collision.GetComponent<Boss>().isDead == false)
            {
                // Play audioclip
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(enemyDiesClip, 1f);

                collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

                collision.GetComponent<Boss>().isDead = true;

                string bossKilled = "Boss" + collision.GetComponent<Boss>().scoreValue.ToString();

                GameManager.Instance.ScoreFunction(bossKilled);

                Destroy(collision.gameObject, 0.5f);

                Destroy(gameObject);
            }
        }
        // Bullet collision with player
        else if (collision.gameObject.tag == "Player" && bulletType == BulletType.enemy)
        {
            if (collision.GetComponent<PlayerController>().shield == false)
            {
                // Play audioclip
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(shipExplosion, 2f);

                // Initiate explosion spite
                collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

                collision.GetComponent<PlayerController>().shield = true;

                collision.GetComponent<PlayerController>().shieldProgress = 0f;

                collision.GetComponent<PlayerController>().isDead = true;

                // Destroy explosion after 0.5 of a second
                Destroy(collision.gameObject, .5f);

                GameManager.Instance.PlayerDeath();
            }
            
            // Destroy bullet
            Destroy(gameObject);
        }
        // Destroy bullet and shield when collision occurs
        else if (collision.gameObject.tag == "Shield")
        {
            collision.GetComponent<Shield>().shieldHealth--;

            if (collision.GetComponent<Shield>().shieldHealth == 2)
            {
                collision.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<Shield>().damageOne;
            }
            else if (collision.GetComponent<Shield>().shieldHealth == 1)
            {
                collision.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<Shield>().damageTwo;
            }
            else
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
        // Destroy bullet if it collides with the world barrier
        else if (collision.gameObject.tag == "WorldBarrier")
        {
            Destroy(gameObject);
        }
    }
}