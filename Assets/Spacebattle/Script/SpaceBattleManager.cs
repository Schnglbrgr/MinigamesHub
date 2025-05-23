using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private WeightedPickerSO spawnRandomEnemy;
    [SerializeField] private PoolQueue poolQueue;

    private Vector2Int map = new Vector2Int(10,20);
    private Vector3 randomSpawnPosition;
    private Vector3 randomSpawnPowerUp;
    public GameObject currentEnemy;
    private GameObject currentPrefab;
    private GameObject currentPowerUp;

    private float timerEnemy;
    private float timerPowerUps;
    private float coolDownPowerUps = 15f;
    public float coolDownEnemySpawn = 5f;
    private int randomSpawn;
    private int randomSpawnPower;
    private int randomPowerUp;
    public int score;

    private void Start()
    {
        Time.timeScale = 1f;
        SpawnEnemies();
        lose_PausedHUD.SetActive(false);
        score = 0;
        scoreText.text = $"Score: {score}";
        timerPowerUps = coolDownPowerUps;

    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        PausedGame();

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
        randomSpawn = Random.Range(2, 8);

        randomSpawnPosition = new Vector3(randomSpawn, 17, -1f);

        currentEnemy = poolQueue.GetObject(spawnRandomEnemy.GetRandomObject(),randomSpawnPosition);

        timerEnemy = coolDownEnemySpawn;
    }

    private void SpawnPowerUps()
    {
        if (timerPowerUps <= 0)
        {
            randomSpawnPower = Random.Range(2, 8);

            randomSpawnPowerUp = new Vector3(randomSpawnPower, 17, -1f);

            randomPowerUp = Random.Range(0, powerUps.Length);

            currentPowerUp = Instantiate(powerUps[randomPowerUp], randomSpawnPowerUp, Quaternion.identity);

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

            speed++;
        }
    }

    void PausedGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            lose_PausedHUD.SetActive(true);

            lose_PausedText.text = "Game Paused";

            restart_Paused.GetComponentInChildren<TMP_Text>().text = "Resume";

            Time.timeScale = 0f;

            restart_Paused.onClick.AddListener(() => StartCoroutine(ResumeGame()));

        }
    }

    IEnumerator ResumeGame()
    {
        Time.timeScale = 0.1f;

        lose_PausedHUD.SetActive(false);

        timerText.text = "3";

        yield return new WaitForSeconds(0.1f);
        timerText.text = "2";

        yield return new WaitForSeconds(0.1f);
        timerText.text = "1";

        yield return new WaitForSeconds(0.1f);
        timerText.text = "";
        Time.timeScale = 1f;

    }

    public void EndGame()
    {
        Destroy(currentPrefab);

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lose";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        Time.timeScale = 0f;

        restart_Paused.onClick.AddListener(Start);
    }
}
