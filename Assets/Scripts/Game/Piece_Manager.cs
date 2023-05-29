using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class Piece_Manager : MonoBehaviour
{
    public Player_Controller inputAction;
    private InputAction fire;
    private RaycastHit2D hit;
    private float distance = 10f;
    [SerializeField] private LayerMask cell;
    [SerializeField] List<TMP_Text> scores;
    public static Piece_Manager instance;
    Sprite_Manager.spriteType winningPlayer;

    private void Awake()
    {
        inputAction = new Player_Controller();
        instance = this;
    }

    private void OnEnable()
    {
        fire = inputAction.Player.Fire;
        fire.Enable();
        fire.performed += Click;
    }

    private void OnDisable()
    {
        fire.Disable();
        fire.performed -= Click;        
    }

    private void Click(InputAction.CallbackContext context)
    {
        RunMove();
        //MinMaxBrain.instance.RunMinMax();
    }

    public void RunMove(Transform aiMove = null) 
    {
        Transform thisMove;
        if (aiMove == null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            hit = Physics2D.Raycast(mousePos, -Vector2.up, distance, cell);
            if (hit.collider is null)
            {
                return;
            }
            thisMove = hit.collider.GameObject().transform;
        }
        else
        {
            thisMove = aiMove.GameObject().transform;
        }        

        if (!Menu_Manager.gameIsPaused)
        {
            if (Turn_Manager.instance.currentAction == Turn_Manager.Actions.add)
            {
                if (thisMove != null && thisMove.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
                {
                    if (Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player1)
                    {
                        thisMove.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.player1);
                    }
                    else
                    {
                        thisMove.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.player2);
                    }
                }
                else
                {
                    return;
                }
            }
            else if(Turn_Manager.instance.currentAction == Turn_Manager.Actions.remove)
            {
                if (thisMove != null && thisMove.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1 && Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player2)
                {
                    thisMove.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.empty);
                }
                else if (thisMove != null && thisMove.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player2 && Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player1)
                {
                    thisMove.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.empty);
                }
                else
                {
                    return;
                }
            }
            CalcScore();
            Turn_Manager.instance.NextTurn();
        }        
    }

    private void CalcScore()
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
        scores[0].text = player1.ToString();
        scores[1].text = player2.ToString();
    }
}
