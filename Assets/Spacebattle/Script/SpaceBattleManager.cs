using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private WeightedPicker pickRandomEnemy;

    public PoolManagerSO enemyPool;
    public PoolManagerSO powerUpPool;

    private Vector2Int map = new Vector2Int(10,20);
    public Vector3 randomSpawnPosition;
    public GameObject[] powerUps;
    private GameObject currentEnemy;
    public GameObject currentPrefabEnemy;
    public GameObject currentPrefabPowerUp;
    private GameObject currentPowerUp;

    private float timerEnemy;
    private float timerPowerUps;
    private float coolDownPowerUps = 15f;
    private float coolDownEnemySpawn = 5f;
    private int randomSpawn;
    private int randomPowerUp;
    public int score;


    private void Start()
    {
        Time.timeScale = 1f;
        lose_PausedHUD.SetActive(false);
        SpawnEnemies();
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
        currentPrefabEnemy = pickRandomEnemy.SelectRandomEnemy();

        currentEnemy = enemyPool.CreateObject(currentPrefabEnemy);

        currentEnemy.GetComponent<SpaceBattleEnemy>().prefab = currentPrefabEnemy;

        currentEnemy.transform.position = PickRandomSpawn();

        currentEnemy.GetComponent<SpaceBattleEnemy>().ResetState();

        timerEnemy = coolDownEnemySpawn;
    }

    public Vector3 PickRandomSpawn()
    {
        randomSpawn = Random.Range(2, 8);

        randomSpawnPosition = new Vector3(randomSpawn, 17, -1f);

        return randomSpawnPosition;
    }

    private void SpawnPowerUps()
    {
        if (timerPowerUps <= 0)
        {
            randomPowerUp = Random.Range(0, powerUps.Length);

            currentPrefabPowerUp = powerUps[randomPowerUp];

            currentPowerUp = powerUpPool.CreateObject(currentPrefabPowerUp);

            currentPowerUp.GetComponent<MovementPowerUps>().prefab = currentPrefabPowerUp;

            currentPowerUp.transform.position = PickRandomSpawn();

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
        lose_PausedHUD.SetActive(false);

        timerText.text = "3";

        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(0.1f);
        timerText.text = "2";

        yield return new WaitForSeconds(0.1f);
        timerText.text = "1";

        yield return new WaitForSeconds(0.1f);
        timerText.text = "";


    }

    public void EndGame()
    {
        Destroy(currentEnemy);

        Destroy(currentPowerUp);

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lose";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        Time.timeScale = 0f;

        restart_Paused.onClick.AddListener(Start);
    }
}
