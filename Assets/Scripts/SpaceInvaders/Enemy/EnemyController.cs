using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Enemy;

    [Header("Bullet variables")]
    public GameObject hitEffect;
    public GameObject bulletPrefab;
    public Vector3 bulletOffset;
    public float fireRate;

    private float fireCoolDown;

    public static EnemyController _instance;
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

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown <= 0)
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
