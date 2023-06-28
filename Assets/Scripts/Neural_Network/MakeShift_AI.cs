using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShift_AI : MonoBehaviour
{
    public static MakeShift_AI instance;
    int rng;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rng = Random.Range(0, Game_Core.instance.boardCells.Count);        
    }

    public void MakeMove()
    {
        while (Game_Core.instance.boardCells[rng].GetComponent<Sprite_Manager>().currentSprite != Sprite_Manager.spriteType.player2) 
        { 
            if (Game_Core.instance.boardCells[rng].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {
                Game_Core.instance.boardCells[rng].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
            }
            rng = Random.Range(0, Game_Core.instance.boardCells.Count);
        }
    }
}
