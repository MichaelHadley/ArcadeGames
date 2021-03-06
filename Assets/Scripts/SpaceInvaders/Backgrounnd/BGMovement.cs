﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    //public float bgSpeedX;
    public float bgSpeedY;
    public Renderer bgRender;

    // Update is called once per frame
    void Update()
    {
        Material mat = bgRender.material;
        
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;

        bgRender.material.mainTextureOffset += new Vector2(0, bgSpeedY * Time.deltaTime);
    }
}
