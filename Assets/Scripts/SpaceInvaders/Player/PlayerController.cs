using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player variables")]
    public GameObject Player;
    public float moveSpeed;
    public Rigidbody2D rb;
    
    [Header("Bullet variables")]
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;
    public float fireRate;

    private float fireCoolDown;
    private Vector2 Movement;

    public static PlayerController _instance;
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

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Shoot();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
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
