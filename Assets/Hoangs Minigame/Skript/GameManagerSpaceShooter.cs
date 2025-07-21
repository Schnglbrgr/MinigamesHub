using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpaceShooter : MonoBehaviour
{
    public static GameManagerSpaceShooter instance;

    public int score;
    public bool gameOver = false;

    void Awake()
    {
        instance = this;
    }
}
