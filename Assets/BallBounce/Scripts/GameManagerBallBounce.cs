using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBallBounce : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject ballPrefab;
    [Space(5)]

    [Header("UI")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text statsSpeedText;
    [SerializeField] TMP_Text statsSizeText;
    [Space(5)]

    [Header("PowerUps")]
    [SerializeField] WeightedPuPickerSO_BallBounce powerUpPicker;
    private float spawnX = 7.5f;

    private PlatformController platform;
    private int score;

    public bool isPaused { get; private set; }
    private bool isPauseable = true;
    private float previousTimeScale = 1f;

    private void Awake()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu?.SetActive(false);
        gameOverText?.SetActive(false);        
    }


    void Start()
    {
        StartCoroutine(SpawnPowerUps());
        score = 0;
        UpdateScoreText();
        SpawnBall();        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();           
        }
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
        isPauseable = false;
        
        gameOverText?.SetActive(true);
        scoreText.rectTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        scoreText.transform.localPosition = new Vector2(0, 50);
    }


    void PauseGame()
    {        
        if (!isPaused && isPauseable)
        {
            previousTimeScale = Time.timeScale;

            Time.timeScale = 0;
            pauseMenu?.SetActive(true);
            isPaused = true;
            UpdateStatsText();
        }
        else if (isPaused && isPauseable)
        {
            Time.timeScale = previousTimeScale;
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
        Time.timeScale = previousTimeScale;
        pauseMenu?.SetActive(false);
        isPaused = false;
    }


    private void UpdateStatsText()
    {
        platform = FindAnyObjectByType<PlatformController>();
        float speed = platform.speed;
        float size = platform.scaleStat;

        statsSpeedText.text = "Speed: " + speed;
        statsSizeText.text = "Size: " + size;
    }

    
    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            if (!isPaused)
            {                
                float randomX = Random.Range(-spawnX, spawnX);
                float randomTime = Random.Range(6f, 12f);

                yield return new WaitForSeconds(randomTime);

                PowerUpEffect result = powerUpPicker.GetRandomPowerUp();
                Instantiate(result.powerUpPrefab, new Vector2(randomX, 6), Quaternion.identity);

            }
            else
            {
                yield return null;
            }
        }            
    }        
}
