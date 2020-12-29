using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public float bgSpeedX;
    public float bgSpeedY;
    public Renderer bgRender;

    // Update is called once per frame
    void Update()
    {
        bgRender.material.mainTextureOffset += new Vector2(Time.deltaTime * bgSpeedX, Time.deltaTime * bgSpeedY);
    }
}
