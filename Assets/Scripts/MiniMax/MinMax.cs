using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    public static MinMax instance;

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

    }
}
