using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool pause;
    public static bool unpause;

    public static bool moveForwards;
    public static bool moveBackwards;
    public static bool moveLeft;
    public static bool moveRight;
   

    // Update is called once per frame
    void Update()
    {
        moveForwards = Input.GetKeyDown(KeyCode.W);
        moveBackwards = Input.GetKeyDown(KeyCode.S);
        moveLeft = Input.GetKeyDown(KeyCode.A);
        moveRight = Input.GetKeyDown(KeyCode.D);

        //movement
        if (moveForwards)
        {
            Frogger.Instance.MoveForwards();
        }
<<<<<<< HEAD

        // SpaceInvaders controls
        //PlayerController.Instance.Movement();
        //PlayerController.Instance.Shoot();
=======
        if (moveLeft)
        {
            Frogger.Instance.MoveLeft();
        }
        if (moveRight)
        {
            Frogger.Instance.MoveRight();
        }
        if (moveBackwards)
        {
            Frogger.Instance.MoveBackwards();
        }
>>>>>>> 42a7a1a7f7b0bb975a9b932287a5e20b7e2606dd

        //Pause menu keypress code here
        pause = Input.GetKey(KeyCode.Escape);
    }
}
