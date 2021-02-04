using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool moveForwards;
    public static bool moveBackwards;
    public static bool moveLeft;
    public static bool moveRight;


    // Update is called once per frame
    void Update()
    {
        moveForwards = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.I);
        moveBackwards = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        moveLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        // Edit the above to be able to select individual settings in the settings menu
        // e.g userforward key to replace KeyCode.W
        // https://www.studica.com/blog/custom-input-manager-unity-tutorial reference this to create your own version



        //movement
        if (moveForwards)
        {
            Frogger.Instance.MoveForwards();
        }
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
    }
}
