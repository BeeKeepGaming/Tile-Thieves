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
            //Get block Values
            float score = MinMax();

            if(score < bestScore)
            {
                bestScore = score;
                tempspot = board[i].transform;
            }
        }
    }

    private float MinMax()
    {
        return 0;
    }
}
