using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificulty_Select : MonoBehaviour
{
    public static Dificulty_Select instance;
    public int difficulty;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0f;
    }

    public void SetDifficulty(int iDifficulty)
    {
        difficulty = iDifficulty;
    }
    public void SetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
