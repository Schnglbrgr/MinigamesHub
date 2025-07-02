using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpaceBattleManager : MonoBehaviour
{
    [Header ("----Components----")]
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private WeightedPickerSO pickRandomEnemy;
    [SerializeField] private WeightedPickerSO pickRandomPowerUp;
    [SerializeField] private GameObject controlHUD;
    [SerializeField] private GameObject keyboardButton;

    public PoolManagerSO poolManager;
    public GameObject bossEnemy;
    public GameObject currentPrefabPowerUp;
    public GameObject[] powerUps;

    private GameObject currentEnemy;
    private GameObject currentPrefabEnemy;
    private GameObject currentPowerUp;
    private AudioControllerSpaceBattle audioController;
    private PlayerInput playerInput;
    private UltimateAttackSpaceBattle ultimateAttack;

    [Header("----Variables----")]
    private Vector2Int map = new Vector2Int(10, 20);
    public Vector3 randomSpawnPosition;
    private float timerEnemy;
    private float timerPowerUps;
    private float coolDownPowerUps = 15f;
    private float coolDownEnemySpawn = 5f;
    public int score;
    public bool bossActive;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        ultimateAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateAttackSpaceBattle>();
    }

    private void Start()
    {
        controlHUD.SetActive(true);

        EventSystem.current.SetSelectedGameObject(keyboardButton);

        Time.timeScale = 0f;
    }

    public void Keyboard()
    {
        playerInput.defaultControlScheme = "Keyboard";

        StartGame();
    }

    public void Gamepad()
    {
        playerInput.defaultControlScheme = "GamePad";

        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;

        lose_PausedHUD.SetActive(false);

        bossActive = false;

        score = 0;

        scoreText.text = $"Score: {score}";

        timerPowerUps = coolDownPowerUps;

        controlHUD.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        if (timerEnemy > 0)
        {
            timerEnemy -= Time.deltaTime;
        }

        if (timerPowerUps > 0)
        {
            timerPowerUps -= Time.deltaTime;
        }

        if (timerEnemy <= 0)
        {
            SpawnEnemies();
        }

        SpawnPowerUps();
    }

    public void SpawnEnemies()
    {
        if (!bossActive)
        {
            currentPrefabEnemy = pickRandomEnemy.SelectRandomObject();

            currentEnemy = poolManager.CreateObject(currentPrefabEnemy);

            timerEnemy = coolDownEnemySpawn;
        }
    }

    private void SpawnPowerUps()
    {
        if (timerPowerUps <= 0 && !bossActive && !ultimateAttack.ultimateActive)
        {
            currentPrefabPowerUp = pickRandomPowerUp.SelectRandomObject();

            currentPowerUp = poolManager.CreateObject(currentPrefabPowerUp);

            timerPowerUps = coolDownPowerUps;
        }
    }

    public bool InsideMap(Transform player)
    {
        if (player.position.x >= 0 && player.position.x < map.x && player.position.y >= 0 && player.position.x < map.y)
        {
            return true;
        }

        return false;
    }

    public void LevelUp(float speed)
    {
        if (score % 100 == 0 && score != 0)
        {
            coolDownEnemySpawn = Mathf.Max(coolDownEnemySpawn - 0.2f, 0.5f);

            bossEnemy.SetActive(true);

            poolManager.Return(currentPrefabEnemy, currentEnemy);

            bossActive = true;

            speed++;
        }
    }

    public void PausedGame()
    {
        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "Game Paused";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Resume";

        EventSystem.current.SetSelectedGameObject(restart_Paused.gameObject);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        lose_PausedHUD.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Start();
    }

    public void EndGame()
    {
        Destroy(currentEnemy);

        Destroy(currentPowerUp);

        bossEnemy.SetActive(false);

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lose";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        audioController.MakeSound(audioController.gameOver);

        Time.timeScale = 0f;

        restart_Paused.onClick.AddListener(Start);
    }
}
