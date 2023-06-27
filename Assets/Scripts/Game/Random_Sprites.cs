using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Sprites : MonoBehaviour
{
    public static Random_Sprites instance;
    public List<Sprite> backgroundSprites;
    public List<Sprite> blockerSprites;
    public GameObject cellBGPrefab;
    public GameObject cellPPrefab;
    public int rngSprite;

    private void Awake()
    {
        instance = this;
        rngSprite = Random.Range(0, backgroundSprites.Count);
    }

    private void Start()
    {
        Camera cam = Camera.main;
        cellBGPrefab.GetComponent<SpriteRenderer>().sprite = backgroundSprites[rngSprite];
        cellPPrefab.GetComponent<Sprite_Manager>().block = blockerSprites[rngSprite];
        //switch (rngSprite)
        //{
        //    case 0:
        //        cam.backgroundColor = Color.white;
        //        break;
        //    case 1:
        //        cam.backgroundColor= Color.blue;
        //        break;
        //    case 2:
        //        cam.backgroundColor= Color.green;
        //        break;
        //    case 3:
        //        cam.backgroundColor= Color.magenta;
        //        break;
        //    case 4:
        //        cam.backgroundColor= Color.red;
        //        break;
        //}
    }
}