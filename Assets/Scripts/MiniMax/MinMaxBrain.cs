using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class MinMaxBrain : MonoBehaviour
{
    public static MinMaxBrain instance;

    private List<GameObject> board;
    private List<Zone> zones;
    [SerializeField] int maxDepth;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        board = Game_Core.instance.boardCells;
        zones = Game_Core.instance.zones;
    }

    public void RunMinMax()
    {
        float score = 0;
        Transform tempspot = null;
        float bestScore = -Mathf.Infinity;

        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                score = MinMax(board[i],0,false);
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;

                if (score < bestScore)
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
            return 0;
        }

        if (MaximizingPlayer)
        {
            float maxScore = -Mathf.Infinity;
            for(int i  = 0; i < board.Count; ++i)
            {
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                score = MinMax(minMaxboard, depth - 1, false);
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;
                maxScore = Mathf.Max(maxScore, score);                
            }
            return maxScore;
        }
        else
        {
            float minScore = Mathf.Infinity;
            for(int i = 0;i < board.Count; ++i)
            {
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player1;
                score = MinMax(minMaxboard,depth - 1, true);
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;
                minScore = Mathf.Min(minScore, score);
            }
            return minScore;
        }
    }
}
