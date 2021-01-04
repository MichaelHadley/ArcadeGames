using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public GameObject hitEffect;
    public float bulletSpeed = 5f;
    public float timer = 1f;

    private void Update()
    {
        Movement();

        DestroyOverTime();
    }

    private void Movement()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        pos -= velocity;

        transform.position = pos;
    }

    private void DestroyOverTime()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensures that the bullet does not collider with the player
        Physics2D.IgnoreCollision(EnemyController.Instance.Enemy.GetComponent<Collider2D>(), EnemyController.Instance.bulletPrefab.GetComponent<Collider2D>());

        if (collision.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            Destroy(gameObject);
        }
    }
}
