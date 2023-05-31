using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turn_Manager : MonoBehaviour
{
    public enum Players
    {
        player1, player2
    }
    public enum Actions
    {
        add, remove 
    }

    public Players currentPlayer;
    public Actions currentAction;
    public static Turn_Manager instance;
    [SerializeField] List<TMP_Text> turn;
    [SerializeField] int endGameScene;
    [SerializeField] bool playingAgainstAI;

    private void Start()
    {
        instance = this;
        currentPlayer = (Players)Random.Range(0, 2);
        if (currentPlayer == Players.player1)
        {
            turn[0].text = currentAction.ToString();
            turn[1].text = " ";
        }
        else
        {
            turn[1].text = currentAction.ToString();
            turn[0].text = " ";
            if (playingAgainstAI == true)
            {
                MinMaxBrain.instance.RunMinMax();
            }
        }
    }

    public void NextTurn()
    {
        if (EndGame())
        {
            Menu_Manager.instance.ChangeScene(endGameScene);
        }
        for (int i = 0; i < Game_Core.instance.zones.Count; i++)
        {
            if (Game_Core.instance.zones[i].GetBonusMove(Zone.Actions.remove) == Zone.Actions.remove)
            {
                currentAction = Actions.remove;
                if (currentPlayer == Players.player1)
                {
                    turn[0].text = currentAction.ToString();
                    turn[1].text = " ";
                }
                else
                {
                    turn[1].text = currentAction.ToString();
                    turn[0].text = " ";
                    if (playingAgainstAI == true)
                    {
                        MinMaxBrain.instance.RunMinMax();
                    }
                }
                if (GameStateQuit())
                {
                    Menu_Manager.instance.ChangeScene(endGameScene);
                }
                return;
            }
        }

        for (int i = 0; i < Game_Core.instance.zones.Count; i++)
        {
            if (Game_Core.instance.zones[i].GetBonusMove(Zone.Actions.add) == Zone.Actions.add)
            {
                currentAction = Actions.add;
                if (currentPlayer == Players.player1)
                {
                    turn[0].text = currentAction.ToString();
                    turn[1].text = " ";
                }
                else
                {
                    turn[1].text = currentAction.ToString();
                    turn[0].text = " ";
                    if (playingAgainstAI == true)
                    {
                        MinMaxBrain.instance.RunMinMax();
                    }
                }
                return;
            }
        }

        currentAction = Actions.add;
        if (currentPlayer == Players.player1)
        {
            currentPlayer = Players.player2;
            turn[1].text = currentAction.ToString();
            turn[0].text = " ";
            if (playingAgainstAI == true)
            {
                MinMaxBrain.instance.RunMinMax();
            }
        }
        else
        {
            currentPlayer = Players.player1;
            turn[0].text = currentAction.ToString();
            turn[1].text = " ";
        }
    }

    public bool EndGame()
    {
        for (int i = 0;i < Game_Core.instance.boardCells.Count; i++)
        {
            if (Game_Core.instance.boardCells[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
            {
                return false;
            }
        }
        return true;
    }

    public bool GameStateQuit()
    {
        int player1 = 0;
        int player2 = 0;

        for (int i = 0; i < Game_Core.instance.boardCells.Count; i++)
        {
            if (Game_Core.instance.boardCells[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1)
            {
                player1++;
            }
            else if (Game_Core.instance.boardCells[i].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player2)
            {
                player2++;
            }
        }

        if(player1 == 0 ||  player2 == 0)
        {
            return true;
        }
        return false;
    }
}
