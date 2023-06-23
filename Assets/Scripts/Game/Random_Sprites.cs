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
        cellBGPrefab.GetComponent<SpriteRenderer>().sprite = backgroundSprites[rngSprite];
        cellPPrefab.GetComponent<Sprite_Manager>().block = blockerSprites[rngSprite];
    }
}