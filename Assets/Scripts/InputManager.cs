using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool pause;
    public static bool unpause;

    public bool moveUp;

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKeyDown(KeyCode.W);

        if (moveUp)
        {
            Frogger.Instance.MoveUp();
        }

        // SpaceInvaders controls
        //PlayerController.Instance.Movement();
        //PlayerController.Instance.Shoot();

        //Pause menu keypress code here
        pause = Input.GetKey(KeyCode.Escape);
    }
}
