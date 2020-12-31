using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        player, enemy
    }

    public BulletType bulletType;

    public GameObject hitEffect;
    public float bulletSpeed = 5f;


    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        if (bulletType == BulletType.player)
        {
            pos += velocity;
        }
        else
        {
            pos -= velocity;
        }

        transform.position = pos;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && bulletType == BulletType.player)
        {
            GameObject explosion = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && bulletType == BulletType.enemy)
        {
            GameObject explosion = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "WorldBarrier")
        {
            Destroy(gameObject);
        }
    }
}
