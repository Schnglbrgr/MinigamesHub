using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject pauseMenu;
    private int score;
    private bool isPaused = false;
    internal static readonly object instance;

    private void Awake()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu?.SetActive(false);
        gameOverText?.SetActive(false);        
    }


    void Start()
    {
        score = 0;
        UpdateScoreText();
        SpawnBall();
    }

    
    void SpawnBall()
    {
        Instantiate(ballPrefab, ballSpawner.position, Quaternion.identity);
    }


    void UpdateScoreText()
    {
        scoreText.SetText("Score: " + score);
    }


     public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }


    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverText?.SetActive(true);
        scoreText.rectTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        scoreText.transform.localPosition = new Vector2(0, 50);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Application.targetFrameRate = 60;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Application.targetFrameRate = 144;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {        
        if (!isPaused)
        {
            Time.timeScale = 0;
            pauseMenu?.SetActive(true);
            isPaused = true;
        }
        else if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu?.SetActive(false);
            isPaused = false;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("BallBounce");
        Time.timeScale = 1;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu?.SetActive(false);
        isPaused = false;
    }
}
