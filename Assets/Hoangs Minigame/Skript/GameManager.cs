using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public bool gameOver = false;

    void Awake()
    {
        instance = this;
    }
}
