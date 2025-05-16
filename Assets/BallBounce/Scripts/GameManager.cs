using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ballPrefab;    
    [Space(5)]

    [Header("UI")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text powerUpTimer;
    [Space(5)]

    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject powerUp;
    private int score;
    private bool isPaused = false;
    private float time;
    private float timer = 8f;


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
        time = timer;
        UpdateScoreText();
        SpawnBall();
        UpdateTimerText();
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

        PowerUpTimer();
    }


    void PowerUpTimer()
    {               
        if (time >= 0)
        {
            time -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            SpawnPowerUp();
            time = timer;
        }
        
    }

    void UpdateTimerText()
    {
        powerUpTimer.text = "PowerUp in: " + time.ToString("0.0");
    }


    void SpawnPowerUp()
    {
        float minX = -7.5f;
        float maxX = 7.5f;
        float spawnX = Random.Range(minX, maxX);
        Instantiate(powerUp, new Vector2(spawnX, 6), Quaternion.identity);
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


    public void PauseGame()
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
    }
}
