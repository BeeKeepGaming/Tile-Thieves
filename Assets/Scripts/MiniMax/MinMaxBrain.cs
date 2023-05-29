using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MinMaxBrain : MonoBehaviour
{
    public static MinMaxBrain instance;

    [SerializeField] private List<GameObject> board;
    [SerializeField] private List<Zone> zones;
    public int maxDepth;

    private void Awake()
    {
        instance = this;
        zones = new List<Zone>(Game_Core.instance.zones);
        board = new List<GameObject>(Game_Core.instance.boardCells);

        switch (Dificulty_Select.instance.difficulty)
        {
            case 2:
                maxDepth = 2;
                break;
            case 4:
                maxDepth = 4;
                break;
            case 6:
                maxDepth = 6;
                break;
        }
    }

    public void RunMinMax()
    {
        Debug.Log("running minmax");
        float score;
        Transform tempspot = null;
        float bestScore = -Mathf.Infinity;

        if (Turn_Manager.instance.currentAction == Turn_Manager.Actions.remove && Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player2)
        {
            //remove Logic
            for (int i = 0; i < board.Count; i++)
            {
                if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1)
                {
                    tempspot = board[i].transform;
                    Piece_Manager.instance.RunMove(tempspot);
                    //Can Add Better Remove Logic <-
                    return;
                }
            }
        }        
        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {                                
                board[i].GetComponent<Sprite_Manager>().currentSprite = Sprite_Manager.spriteType.player2;
                
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
        ReCalcZones();
        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player2)
            {
                for(int j = 0 ; j < zones.Count; j++)
                {
                    bestScore += zones[j].GetScore(i);                    
                }
            }
            if (board[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1)
            {
                for (int j = 0; j < zones.Count; j++)
                {
                    bestScore -= zones[j].GetScore(i);
                }
            }
        }
        return bestScore;
    }

    private void ReCalcZones()
    {
        for(int i = 0;i < zones.Count; i++)
        {
            zones[i].CalcZoneValue();
        }
    }
}