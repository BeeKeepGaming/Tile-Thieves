using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{
    public enum Actions
    {
        add, remove, none
    }

    public enum ZoneType
    {
        line, block
    }

    public List<int> blocks = new List<int>();
    private ZoneType zoneType;
    private bool played = false;

    public Zone(ZoneType zType)
    {
        zoneType = zType;
    }

    public void AddBlock(int block)
    {
        blocks.Add(block);
    }

    public Actions GetBonusMove(Actions check)
    {
        int player1 = 0;
        int player2 = 0;

        for (int i = 0; i < blocks.Count; i++)
        {
            if(Game_Core.instance.boardCells[blocks[i]].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1)
            {
                player1++;
            }
            else if (Game_Core.instance.boardCells[blocks[i]].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player2)
            {
                player2++;
            }
        }
        if(player1 < blocks.Count && player2 < blocks.Count)
        {
            played = false;
            return Actions.none;
        }

        if(played == true)
        {
            return Actions.none;
        }

        if (zoneType == ZoneType.block)
        {
            if (check == Actions.add)
            {
                played = true;
            }
            return Actions.add;
        }
        else 
        {
            if (check == Actions.remove)
            {
                played = true;
            }        
            return Actions.remove;
        }
    }
}
