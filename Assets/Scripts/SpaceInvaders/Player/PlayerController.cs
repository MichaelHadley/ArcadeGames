using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Player.transform.position += transform.right * (movementSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Player.transform.position -= transform.right * (movementSpeed * Time.deltaTime);
        }
    }
}
