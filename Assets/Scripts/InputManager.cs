using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;

    public static bool pause;
    public static bool unpause;
    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        
        //Pause menu keypress code here
        pause = Input.GetKey(KeyCode.Escape);
    }
}
