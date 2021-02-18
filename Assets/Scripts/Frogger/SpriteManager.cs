using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite leftLilyPad;
    public Sprite rightLilyPad;
    public Sprite upLilyPad;
    public Sprite downLilyPad;

    private SpriteRenderer spriteRenderer;
    
    public void UpdateSprite()
    {
        int rotation = Random.Range(0, 4);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == rightLilyPad && rotation == 0)
        {
            spriteRenderer.sprite = rightLilyPad;
        }
        else if (spriteRenderer.sprite == rightLilyPad && rotation == 1)
        {
            spriteRenderer.sprite = upLilyPad;
        }
        else if (spriteRenderer.sprite == rightLilyPad && rotation == 2)
        {
            spriteRenderer.sprite = downLilyPad;
        }
        else if (spriteRenderer.sprite == rightLilyPad && rotation == 3)
        {
            spriteRenderer.sprite = leftLilyPad;
        }
    }
}
