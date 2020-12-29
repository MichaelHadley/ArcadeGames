using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 Movement;

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        //Movement.y = Input.GetAxisRaw("Vertical");
        //PlayerMovement();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Player.transform.position += transform.right * (moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Player.transform.position -= transform.right * (moveSpeed * Time.deltaTime);
        }
    }
}
