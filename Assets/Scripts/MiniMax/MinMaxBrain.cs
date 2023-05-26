using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class MinMaxBrain : MonoBehaviour
{
    public static MinMaxBrain instance;

    [SerializeField] private List<GameObject> board;
    private List<Zone> zones;
    [SerializeField] int maxDepth;

    private void Awake()
    {
        instance = this;        
    }

    public void RunMinMax()
    {
        board = new List<GameObject>(Game_Core.instance.boardCells);
        zones = new List<Zone>(Game_Core.instance.zones);
        Debug.Log("running minmax");
        float score = 0;
        Transform tempspot = null;
        float bestScore = -Mathf.Infinity;

        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                //Add Remove check
                score = MinMax(board[i],0,false);
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;

                if (score > bestScore)
                {
                    bestScore = score;
                    tempspot = board[i].transform;
                }
            }
        }
        Piece_Manager.instance.RunMove(tempspot);
    }

    private float MinMax(GameObject minMaxboard, int depth, bool MaximizingPlayer)
    {
        float score = 0;
        if(depth > maxDepth)
        {
            float utilScore = Utility();
            
            return utilScore;
        }

        if (MaximizingPlayer)
        {
            float maxScore = -Mathf.Infinity;
            for(int i  = 0; i < board.Count; ++i)
            {
                if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
                {
                    board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                    //Add Remove Check
                    score = MinMax(minMaxboard, depth + 1, false);
                    board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;
                    maxScore = Mathf.Max(maxScore, score);
                }
            }
            return maxScore;
        }
        else
        {
            float minScore = Mathf.Infinity;
            for(int i = 0;i < board.Count; ++i)
            {
                if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
                {
                    board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player1;
                    //Add Remove Check
                    score = MinMax(minMaxboard, depth + 1, true);
                    board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;
                    minScore = Mathf.Min(minScore, score);
                }
            }
            return minScore;
        }
    }

    private float Utility()
    {
        float bestScore = 0;

        for (int i = 0; i < zones.Count; i++)
        {
            bestScore += zones[i].GetZoneValue();
        }
        
        return bestScore;
    }
}

/*for (int j = 0; j < board.Count; j++)
        {
            if(player == Turn_Manager.Players.player1)
            {
                for (int i = 0; i < zones.Count; i++)
                {
                    if (zones[i].GetBonusMove(Zone.Actions.add) == Zone.Actions.add)
                    {
                        scoreP -= 2;
                    }
                    else if (zones[i].GetBonusMove(Zone.Actions.remove) == Zone.Actions.remove)
                    {
                        scoreP -= 2;
                    }
                }
            }
            else
            {
                for (int i = 0; i < zones.Count; i++)
                {
                    if (zones[i].GetBonusMove(Zone.Actions.add) == Zone.Actions.add)
                    {
                        scoreAi += 2;
                    }
                    else if (zones[i].GetBonusMove(Zone.Actions.remove) == Zone.Actions.remove)
                    {
                        scoreAi += 2;
                    }
                }
            }
    }
 */