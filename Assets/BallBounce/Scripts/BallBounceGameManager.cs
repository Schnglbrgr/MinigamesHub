using System.Collections;
using TMPro;
using UnityEngine;

public class BallBounceGameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject ballPrefab;
    [Space(5)]

    //[Header("UI")]
    //[SerializeField] GameObject pauseMenu;
    //[SerializeField] GameObject gameOverUI;
    //[SerializeField] TMP_Text scoreText;
    //[SerializeField] TMP_Text gameOverScoreText;
    //[SerializeField] TMP_Text gameOverHighScoreText;
    //[SerializeField] TMP_Text pauseHighScoreText;
    //[SerializeField] TMP_Text livesText;
    //[SerializeField] TMP_Text statsSpeedText;
    //[SerializeField] TMP_Text statsSizeText;
    //[SerializeField] CanvasGroup gameOverCanvasGroup;
    //[SerializeField] private float gameOverFadeDuration = 1.0f;
    //[Space(5)]

    [Header("PowerUp Settings")]
    [SerializeField] WeightedPuPickerSO_BallBounce powerUpPicker;
    [SerializeField] BallBouncePoolManager poolManager;
    [SerializeField] float minSpawnTime = 6f;
    [SerializeField] float maxSpawnTime = 12f;
    public int lives;
    [Space(5)]

    [Header("Pause Settings")]
    public bool isPaused;
    public bool isPauseable = true;
    

    [HideInInspector] public int score { get; private set; }
    [HideInInspector] public float previousTimeScale { get; private set; } = 1f;

    private PlatformController platform;
    private BallBounceUiManager uiManager;    



    private void Awake()
    {
        Time.timeScale = 1;
        platform = FindAnyObjectByType<PlatformController>();
        uiManager = FindAnyObjectByType<BallBounceUiManager>();
        isPaused = false;
        //pauseMenu?.SetActive(false);
        //gameOverUI?.SetActive(false);        
    }


    void Start()
    {
        lives = 0;
        score = 0;
        //pauseHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        StartCoroutine(SpawnPowerUps());
        //UpdateScoreText();
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


    //public void UpdateScoreText()
    //{
    //    scoreText.text = "Score: " + score;
    //    livesText.text = "Lives: " + lives;
    //}


     public void IncreaseScore()
    {
        score++;
        uiManager.UpdateScoreText();
    }


    public void EndGame()
    {
        StartCoroutine(uiManager.GameOverFadeIn());  

        if(score > PlayerPrefs.GetInt(uiManager.highscore, 0))
        {
            uiManager.SaveHighscore();
            //gameOverHighScoreText.text = $"Highscore: {score}";
        }
        uiManager.LoadHighscore();
    }
    

    void PauseGame()
    {        
        if (!isPaused && isPauseable)
        {
            previousTimeScale = Time.timeScale;
            platform.isMoveable = false;
            Time.timeScale = 0;
            uiManager.pauseMenuUi?.SetActive(true);
            isPaused = true;
            uiManager.UpdateStatsText();
        }
        else if (isPaused && isPauseable)
        {
            Time.timeScale = previousTimeScale;
            uiManager.pauseMenuUi?.SetActive(false);
            isPaused = false;
            platform.isMoveable = true;
        }
    }


    //public void RestartButton()
    //{
    //    SceneManager.LoadScene("BallBounce");
    //    Time.timeScale = 1;
    //}


    //public void ResumeButton()
    //{
    //    Time.timeScale = previousTimeScale;
    //    pauseMenu?.SetActive(false);
    //    isPaused = false;
    //    platform.isMoveable = true;
    //}


    //private void UpdateStatsText()
    //{        
    //    float speed = platform.speed;
    //    float size = platform.scaleStat;

    //    statsSpeedText.text = $"Speed: { speed}";
    //    statsSizeText.text = $"Platform Size: {size}";
    //    pauseHighScoreText.text = $"Highscore: {PlayerPrefs.GetInt("HighScore", 0)}";
    //}

    
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


    //IEnumerator GameOverFadeIn()
    //{
    //    Time.timeScale = 0f;
    //    scoreText.gameObject.SetActive(false);
    //    gameOverCanvasGroup.alpha = 0f;
    //    gameOverUI?.SetActive(true);
    //    isPauseable = false;
    //    platform.isMoveable = false;
    //    isPaused = true;



    //    float elapsed = 0;
    //    while (elapsed < gameOverFadeDuration)
    //    {
    //        elapsed += Time.unscaledDeltaTime;

    //        gameOverCanvasGroup.alpha = Mathf.Clamp01(elapsed / gameOverFadeDuration);

    //        float t = Mathf.Clamp01(elapsed / gameOverFadeDuration);
    //        float animatedScore = Mathf.Lerp(0, score, t); 

    //        gameOverScoreText.text = $"Score: {animatedScore:F0}";

    //        yield return null;
    //    }
    //    gameOverScoreText.text = $"Score: {score}";
    //}
}
