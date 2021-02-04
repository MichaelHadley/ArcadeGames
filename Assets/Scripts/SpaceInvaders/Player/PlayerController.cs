using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player variables")]
    public GameObject player;
    public float moveSpeed;
    public Rigidbody2D rb;
    
    [Header("Bullet variables")]
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;
    public float fireRate;

    private float fireCoolDown;
    private Vector2 movement;
    private int numOfLives = 3;


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
        DestroyOffScreen();
    }

    private void DestroyOffScreen()
    {
        Vector3 playerRelPos = Camera.main.WorldToViewportPoint(transform.position);
        if (playerRelPos.x < 0f || playerRelPos.x > 1)
        {
            FroggerManager.Instance.Death();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
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
        }
    }
}
