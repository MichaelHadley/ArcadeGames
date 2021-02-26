using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    [Header("Bullet variables")]
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;

    [Header("Fire Rate Values")]
    public float minFireRateTime = 1.0f;
    public float maxFireRateTime = 3.0f;
    public float fireRateWaitTime = 3.0f;

    [Header("Enemy Movement")]
    public float speed;

    [Header("Sprites")]
    public Sprite explodedShipImage;

    [Header("Enemy Destroyed")]
    public bool isDead;
    public int scoreValue;

    [Header("Audio")]
    public AudioClip enemyBulletClip;
    public AudioSource bossMovementClip;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        bossMovementClip.GetComponent<AudioSource>();
        bossMovementClip.Play(0);
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fireRateWaitTime = Time.time + Random.Range(minFireRateTime, maxFireRateTime);
    }

    private void Update()
    {
        MoveBoss();
        EnemyFire();
    }

    void MoveBoss()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(speed * Time.deltaTime, 0, 0);

        pos += velocity;

        transform.position = pos;
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

                // Play audioclip
                gameObject.GetComponent<AudioSource>().PlayOneShot(enemyBulletClip, .5f);

                // Create bullet
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Switch direction on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.name == "RightWorldBarrier")
            {
                Destroy(gameObject, 1f);
            }
        }
    }
}
