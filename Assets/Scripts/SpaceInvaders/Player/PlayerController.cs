using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player variables")]
    public GameObject player;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float shieldProgress;
    public float shieldDuration;
    public bool shield = true;

    [Header("Sprites")]
    public Sprite playerSprite;

    [Header("Bullet variables")]
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;
    public float fireRate;

    public bool isDead;

    [Header("Audio")]
    public AudioClip playerBulletClip;

    private float fireCoolDown;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    private void Update()
    {
        Movement();
        Shoot();
        RespawnShields();
    }

    private void RespawnShields()
    {
        shieldProgress += Time.deltaTime / shieldDuration;

        if (shieldProgress >= 1)
        {
            if (shield == true)
            {
                shieldProgress = 0f;
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = playerSprite;
                shield = false;
            }
        }
    }

    

    private void FixedUpdate()
    {
        // If dead stop movement so you can't move the exploded sprite
        if (!isDead)
        {
            rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
        }      
    }

    public void Movement()
    {
        Vector3 playerRelPos = Camera.main.WorldToViewportPoint(transform.position);

        // Move the player based on x input, but limit to within the screen
        // 0.2 and 0.98 used to allow for the size of the player
        if(playerRelPos.x <= 0.02f && Input.GetAxisRaw("Horizontal") < 0)
        {
            movement.x= 0;
        }
        else if (playerRelPos.x >= 0.98f && Input.GetAxisRaw("Horizontal") > 0)
        {
            movement.x = 0;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
    }

    public void Shoot()
    {
        fireCoolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireCoolDown <= 0)
        {
            fireCoolDown = fireRate;

            // Bullet fire position based off ship
            Vector3 offset = transform.rotation * bulletOffset;

            // Create bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
            bullet.layer = gameObject.layer;

            // Play bullet audio when shot
            gameObject.GetComponent<AudioSource>().PlayOneShot(playerBulletClip, .5f);
        }
    }
}
