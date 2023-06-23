using System.Collections.Generic;
using UnityEngine;

public class Game_Core : MonoBehaviour
{
    public static Game_Core instance;

    public List<GameObject> boardCells = new List<GameObject>();
    public List<Zone> zones = new List<Zone>();
    [SerializeField] private GameObject cellBGPrefab;
    [SerializeField] private GameObject cellPPrefab;
    [SerializeField] private bool randomMap;

    [SerializeField] private Transform spawnerPos;
    [SerializeField] private int boardSize = 6; //Posibly in options Menu
    [SerializeField] private int boardBlockerC = 1;
    [SerializeField] private float xOffSet;
    [SerializeField] private float yOffSet;

    private void Awake()
    {
        instance = this;        
        DrawBoard(spawnerPos);
        BlockSpawner();
        ZoneGenerator();
    }
    private void DrawBoard(Transform startPos)
    {
        Vector2 pos = startPos.position;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject BGImage = Instantiate(cellBGPrefab, pos, Quaternion.identity);
                GameObject temp = Instantiate(cellPPrefab, pos, Quaternion.identity);
                boardCells.Add(temp);
                temp.transform.parent = this.transform;
                BGImage.transform.parent = this.transform;
                pos.x += xOffSet;                
            }
            pos.x = startPos.position.x;
            pos.y -= yOffSet;
        }
    }

    private void BlockSpawner()
    {
        if (randomMap == true)
        {
            int randomNum;
            for (int i = 0; i < boardBlockerC; i++)
            {
                randomNum = UnityEngine.Random.Range(0, boardCells.Count);

                if (boardCells[randomNum].GetComponent<Sprite_Manager>().currentSprite == Sprite_Manager.spriteType.empty)
                {
                    boardCells[randomNum].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
                }
                else
                {
                    boardCells[randomNum].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
                    i--;
                }
            }
        }
        else
        {
            boardCells[7].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
            boardCells[10].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
            boardCells[25].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
            boardCells[28].GetComponent<Sprite_Manager>().SwapSprite(Sprite_Manager.spriteType.block);
        }        
    }

    private void ZoneGenerator()
    {
        Zone zone;
        for (int i = 0; i < boardSize; i++) // Populate row zones
        {            
            for (int j = 0;j < boardSize - 4; j++)
            {
                zone = new Zone(Zone.ZoneType.line);
                zone.AddBlock((i * boardSize) + j);
                zone.AddBlock((i * boardSize) + j + 1);
                zone.AddBlock((i * boardSize) + j + 2);
                zone.AddBlock((i * boardSize) + j + 3);
                zone.AddBlock((i * boardSize) + j + 4);
                zones.Add(zone);
            }
        }

        for (int i = 0; i < boardSize; i++) //populate col zones
        {
            for (int j = 0; j < boardSize - 4; j++)
            {
                zone = new Zone(Zone.ZoneType.line);
                zone.AddBlock(i + (j * boardSize));
                zone.AddBlock(i + (j * boardSize) + (1 * boardSize));
                zone.AddBlock(i + (j * boardSize) + (2 * boardSize));
                zone.AddBlock(i + (j * boardSize) + (3 * boardSize));
                zone.AddBlock(i + (j * boardSize) + (4 * boardSize));
                zones.Add(zone);
            }
        }

        for (int i = 0; i < boardSize - 1; i++) //populate block zones
        {
            for (int j = 0; j < boardSize - 1; j++)
            {
                zone = new Zone(Zone.ZoneType.block);
                zone.AddBlock((i * boardSize) +j);
                zone.AddBlock((i * boardSize) + j + 1);
                zone.AddBlock((i * boardSize) + j + boardSize);
                zone.AddBlock((i * boardSize) + j + boardSize +1);
                zones.Add(zone);
            }
        }
    }
}
