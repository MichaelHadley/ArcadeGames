using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("Fire Rate Values")]
    public float minFireRateTime = 1.0f;
    public float maxFireRateTime = 3.0f;
    public float fireRateWaitTime = 3.0f;

    [Header("Enemy Movement")]
    public float speed;
    public float secBeforeSpriteChange = 0.5f;

    [Header("Sprites")]
    public Sprite startingImage;
    public Sprite altImage;
    public Sprite explodedShipImage;

    public bool isDead;

    private SpriteRenderer spriteRenderer;
    private float progress;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        fireRateWaitTime = Time.time + Random.Range(minFireRateTime, maxFireRateTime);
    }

    private void Update()
    {
        MoveEnemy();
        EnemyFire();
        ChangeEnemySprite();
    }

    void MoveEnemy()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(speed * EnemyManager.Instance.speedMultiplier 
            * EnemyManager.Instance.direction * Time.deltaTime, 0, 0);

        pos += velocity;

        transform.position = pos;

        if (EnemyManager.Instance.moveDown)
        {
            transform.DOMoveY(pos.y -1.5f, 0.5f);
        }
    }

    // Enemies fire bullets at random times
    private void EnemyFire()
    {
        if (Time.time > fireRateWaitTime)
        {
            if (!isDead)
            {
                fireRateWaitTime = Time.time + Random.Range(minFireRateTime, maxFireRateTime);

                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Switch direction on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When enemy reaches left barrier move down then move right
        if (collision.gameObject.name == "LeftWorldBarrier")
        {
            EnemyManager.Instance.reachedBarrier = true;
        }

        // When enemy reaches right barrier move down then more left
        if (collision.gameObject.name == "RightWorldBarrier")
        {
            EnemyManager.Instance.reachedBarrier = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            // Play exploding ship sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

            // Change to exploded ship image
            collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            // Destroy AlienBullet
            Destroy(gameObject);

            // Wait .5 seconds and then destroy Player
            Destroy(collision.gameObject, 0.5f);
        }
    }

    public void ChangeEnemySprite()
    {
        progress += Time.deltaTime / (secBeforeSpriteChange / EnemyManager.Instance.speedMultiplier);

        if (progress >= 1f)
        {
            if (!isDead)
            {
                progress = 0f;

                if (spriteRenderer.sprite == startingImage)
                {
                    spriteRenderer.sprite = altImage;
                }
                else
                {
                    spriteRenderer.sprite = startingImage;
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.enemyNoise2);
                }
            }
        }
    }  
}