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
    [SerializeField] TMP_Text livesText;
    [SerializeField] TMP_Text statsSpeedText;
    [SerializeField] TMP_Text statsSizeText;
    [Space(5)]

    [Header("PowerUps")]
    [SerializeField] WeightedPuPickerSO_BallBounce powerUpPicker;
    [SerializeField] BallBouncePoolManager poolManager;
    [SerializeField] float minSpawnTime = 6f;
    [SerializeField] float maxSpawnTime = 12f;
    public int lives;    


    private PlatformController platform;
    private int score;

    public bool isPaused { get; private set; }
    private bool isPauseable = true;
    private float previousTimeScale = 1f;

    private void Awake()
    {
        Time.timeScale = 1;
        platform = FindAnyObjectByType<PlatformController>();
        isPaused = false;
        pauseMenu?.SetActive(false);
        gameOverText?.SetActive(false);        
    }


    void Start()
    {
        lives = 0;
        score = 0;
        StartCoroutine(SpawnPowerUps());
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


    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
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
        platform.isMoveable = false;
        
        gameOverText?.SetActive(true);
        scoreText.rectTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        scoreText.transform.localPosition = new Vector2(0, 50);


    }


    void PauseGame()
    {        
        if (!isPaused && isPauseable)
        {
            previousTimeScale = Time.timeScale;
            platform.isMoveable = false;
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
            platform.isMoveable = true;
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
        platform.isMoveable = true;
    }


    private void UpdateStatsText()
    {        
        float speed = platform.speed;
        float size = platform.scaleStat;

        statsSpeedText.text = "Speed: " + speed;
        statsSizeText.text = "Platform Size: " + size;
    }

    
    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            if (!isPaused)
            {                                
                float randomTime = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(randomTime);

                PowerUpEffect result = powerUpPicker.GetRandomPowerUp();
                poolManager.Get(result.powerUpPrefab);
            }
            else
            {
                yield return null;
            }
        }            
    }        
}
