using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallBounceUiManager : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;
    [Space(5)]    

    [Header("Game Over UI")]
    [SerializeField] GameObject gameOverUI;
    [SerializeField] CanvasGroup gameOverCanvasGroup;
    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] TMP_Text gameOverHighScoreText;
    [SerializeField] private float gameOverFadeDuration = 1.0f;
    [Space(5)]

    [Header("Pause Menu UI")]
    public GameObject pauseMenuUi;
    [SerializeField] TMP_Text pauseHighScoreText;
    [SerializeField] TMP_Text statsSpeedText;
    [SerializeField] TMP_Text statsSizeText;

    [HideInInspector]
    public string highscore = "Highscore";
    

    private BallBounceGameManager gameManager;
    private PlatformController platform;



    private void Awake()
    {
        gameManager = FindAnyObjectByType<BallBounceGameManager>();
        platform = FindAnyObjectByType<PlatformController>();
        pauseMenuUi?.SetActive(false);
        gameOverUI?.SetActive(false);
    }

    

    private void Start()
    {
        UpdateScoreText();
        LoadHighscore();
    }



    public void UpdateScoreText()
    {
        scoreText.text = $"Score: {gameManager.score}";
        livesText.text = $"Lives: {gameManager.lives}";
    }



    public void UpdateStatsText()
    { 
        statsSpeedText.text = $"Speed: {platform.speed}";
        statsSizeText.text = $"Platform Size: {platform.scaleStat}";
        LoadHighscore();
    }



    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(highscore, 0);
        gameOverHighScoreText.text = $"Highscore: {PlayerPrefs.GetInt(highscore)}";
    }



    public void RestartButton()
    {
        SceneManager.LoadScene("BallBounce");
        Time.timeScale = 1;
    }



    public void ResumeButton()
    {
        Time.timeScale = gameManager.previousTimeScale;
        pauseMenuUi?.SetActive(false);
        gameManager.isPaused = false;
        platform.isMoveable = true;
    }


    public void SaveHighscore()
    {
        PlayerPrefs.SetInt(highscore, gameManager.score);
        gameOverHighScoreText.text = $"Highscore: {gameManager.score}";
        pauseHighScoreText.text = $"Highscore: {gameManager.score}";
    }



    public void LoadHighscore()
    {
        gameOverHighScoreText.text = $"Highscore: {PlayerPrefs.GetInt(highscore, 0)}";
        pauseHighScoreText.text = $"Highscore: {PlayerPrefs.GetInt(highscore, 0)}";
    }



    public IEnumerator GameOverFadeIn()
    {
        Time.timeScale = 0f;
        scoreText.gameObject.SetActive(false);
        gameOverCanvasGroup.alpha = 0f;
        gameOverUI?.SetActive(true);
        gameManager.isPauseable = false;
        platform.isMoveable = false;
        gameManager.isPaused = true;



        float elapsed = 0;
        while (elapsed < gameOverFadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            gameOverCanvasGroup.alpha = Mathf.Clamp01(elapsed / gameOverFadeDuration);

            float t = Mathf.Clamp01(elapsed / gameOverFadeDuration);
            float animatedScore = Mathf.Lerp(0, gameManager.score, t);

            gameOverScoreText.text = $"Score: {animatedScore:F0}";

            yield return null;
        }
        gameOverScoreText.text = $"Score: {gameManager.score}";
    }
}
