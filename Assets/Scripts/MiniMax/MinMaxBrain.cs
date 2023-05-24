using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxBrain : MonoBehaviour
{
    public static MinMaxBrain instance;

    private List<GameObject> board;
    private List<Zone> zones;

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
        Transform tempspot = null;
        float bestScore = -Mathf.Infinity;

        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                float score = MinMax(board,0,false);
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.empty;

                if (score < bestScore)
                {
                    bestScore = score;
                    tempspot = board[i].transform;
                }
            }
        }
        //Minmax Clicks Here
    }

    private float MinMax(List<GameObject> minMaxboard, int depth, bool isMaximizing)
    {
        //check the Score of the player
        //check the score of the ai
        //compare the scores
        return 0;
    }
}
