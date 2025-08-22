using System.Collections;
using UnityEngine;

public class BallBounceGameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Transform ballSpawner;
    [SerializeField] GameObject ballPrefab;
    [Space(5)]

    [Header("PowerUp Settings")]
    [SerializeField] WeightedPuPickerSO_BallBounce powerUpPicker;
    [SerializeField] BallBouncePoolManager poolManager;
    [SerializeField] float minSpawnTime = 6f;
    [SerializeField] float maxSpawnTime = 12f;
    [SerializeField] float spawnHeight = 6f;
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
    }


    void Start()
    {
        lives = 0;
        score = 0;
        StartCoroutine(SpawnPowerUps());
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

    public void IncreaseScore()
    {
        score++;
        uiManager.UpdateScoreText();
    }


    public void EndGame()
    {
        StartCoroutine(uiManager.GameOverFadeIn());

        if (score > PlayerPrefs.GetInt(uiManager.highscore, 0))
        {
            uiManager.SaveHighscore();
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


    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            if (!isPaused)
            {
                float randomTime = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(randomTime);

                PowerUpEffect result = powerUpPicker.GetRandomPowerUp();
                poolManager.Get(result.powerUpPrefab, SpawnPosition());
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private Vector2 SpawnPosition()
    {
        float rangeX = 7.5f;
        float randomX = Random.Range(-rangeX, rangeX);

        return new Vector2(randomX, spawnHeight);
    }
}
