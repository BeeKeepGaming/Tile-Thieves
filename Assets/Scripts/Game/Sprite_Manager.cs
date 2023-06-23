using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Manager : MonoBehaviour
{
    public enum spriteType 
    {
        empty = 0, player1 = 1, player2 = 2, block = 3
    };
    public spriteType currentSprite;

    private Sprite empty;
    [SerializeField] private Sprite player1;
    [SerializeField] private Sprite player2;
    public Sprite block;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SwapSprite(spriteType.empty);
    }

    public void SwapSprite(spriteType spriteType)
    {
        currentSprite = spriteType;
        switch (spriteType)
        {
            case spriteType.empty:
                spriteRenderer.sprite = empty;
                break;
            case spriteType.player1:
                spriteRenderer.sprite = player1;
                break;
            case spriteType.player2:
                spriteRenderer.sprite = player2;
                break;
            case spriteType.block:
                spriteRenderer.sprite = block;
                break;                
        }
    }
}
