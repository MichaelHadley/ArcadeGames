using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create bullet
        GameObject Bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();

        // Add an instant force impulse to the rigidbody 2d 
        rb.AddForce(firingPoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
