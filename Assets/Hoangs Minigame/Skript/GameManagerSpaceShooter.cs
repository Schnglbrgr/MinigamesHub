using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerSpaceShooter : MonoBehaviour
{
    public static GameManagerSpaceShooter instance;

    public int score;
    public TMP_Text HighscoreText;
    public GameObject GameOverScene;
    public GameObject ScoreText;
    public int Leben;
    public TMP_Text LebenText;
    private GameObject Player;

    void Awake()
    {
        instance = this;
    }

    private void SafeScore()
    {
        if (score > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", score);
            HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Score", 0).ToString();
        }
    }

    void Start()
    {
        HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Score", 0).ToString();
        score = 0;
        Leben = 3;
        LebenText.text = "Leben: " + Leben.ToString();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    public void GameOver()
    { 
        ScoreText.SetActive(false);
        GameOverScene.SetActive(true);
        SafeScore();
        Leben = 0;
    }

    public void LoseLife()
    {
        if (Leben > 1)
        {
            Leben--;
            LebenText.text = "Leben: " + Leben.ToString();
        }
        else
        {
            GameOver();
            LebenText.text = "Leben: " + Leben.ToString();
            Destroy(Player);
        }
    }
}
