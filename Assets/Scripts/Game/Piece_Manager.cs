using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Piece_Manager : MonoBehaviour
{
    public Player_Controller inputAction;
    private InputAction fire;
    private RaycastHit2D hit;
    private float distance = 10f;
    [SerializeField] private LayerMask cell;
    [SerializeField] List<TMP_Text> scores;

    private void Awake()
    {
        inputAction = new Player_Controller();        
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        hit = Physics2D.Raycast(mousePos, -Vector2.up, distance, cell);

        if (!Menu_Manager.gameIsPaused)
        {
            if (Turn_Manager.instance.currentAction == Turn_Manager.Actions.add)
            {
                if (hit.collider != null && hit.collider.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
                {
                    if (Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player1)
                    {
                        hit.collider.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.player1);
                    }
                    else
                    {
                        hit.collider.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.player2);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (hit.collider != null && hit.collider.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player1 && Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player2)
                {
                    hit.collider.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.empty);
                }
                else if (hit.collider != null && hit.collider.GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.player2 && Turn_Manager.instance.currentPlayer == Turn_Manager.Players.player1)
                {
                    hit.collider.GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.empty);
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
