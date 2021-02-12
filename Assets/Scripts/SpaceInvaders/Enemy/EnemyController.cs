using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    private static EnemyController _instance;
    public static EnemyController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyController>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    [Header("Bullet variables")]
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;

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

    [Header("Enemy Destroyed")]
    public bool isDead;
    public int scoreValue;

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

        Vector3 velocity = new Vector3(speed * EnemyManager.Instance.speedMultiplier * EnemyManager.Instance.direction * Time.deltaTime, 0, 0);

        pos += velocity;

        transform.position = pos;

        if (EnemyManager.Instance.moveDown)
        {
            transform.DOMoveY(pos.y - 1.5f, .5f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }

    // Enemies fire bullets at random times
    private void EnemyFire()
    {
        if (Time.time > fireRateWaitTime)
        {
            if (!isDead)
            {
                // Set enemy rate of fire
                fireRateWaitTime = Time.time + (Random.Range(minFireRateTime, maxFireRateTime) / EnemyManager.Instance.speedMultiplier);

                // Bullet fire position based off ship
                Vector3 offset = transform.rotation * bulletOffset;

                // Create bullet
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // Play bullet audio when shot
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.enemyBullet);
            }
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

    // Switch direction on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
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

            if (collision.gameObject.tag == "Shield")
            {
                // Destroy shield if hit by enemy, enemy lives
                Destroy(collision.gameObject);
            }
        }
    }
}