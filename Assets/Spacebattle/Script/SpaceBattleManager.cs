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

    private Vector2Int map = new Vector2Int(10,20);
    private Vector3 randomSpawnPosition;
    private Vector3 randomSpawnPowerUp;
    private GameObject currentEnemy;
    private GameObject currentPrefab;
    private GameObject currentPowerUp;

    private float timerEnemy;
    private float timerPowerUps;
    private float coolDownPowerUps = 15f;
    private float coolDownEnemySpawn = 5f;
    private int randomSpawn;
    private int randomEnemy;
    private int randomSpawnPower;
    private int randomPowerUp;
    public int score;

    private void Start()
    {
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

        randomEnemy = Random.Range(0, enemies.Length);

        Instantiate(enemies[randomEnemy], randomSpawnPosition, Quaternion.identity);    

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
        if (score % 100 == 0)
        {
            coolDownEnemySpawn = Mathf.Max(coolDownEnemySpawn - 0.2f, 0.5f);

            speed++;
        }
    }

    void PausedGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            currentPrefab.GetComponent<PlayerSpaceBattle>().enabled = false;

            currentEnemy.GetComponent<SpaceBattleEnemy>().enabled = false;

            lose_PausedHUD.SetActive(true);

            lose_PausedText.text = "Game Paused";

            restart_Paused.GetComponentInChildren<TMP_Text>().text = "Resume";

            restart_Paused.onClick.AddListener(() => StartCoroutine(ResumeGame()));

        }
    }

    IEnumerator ResumeGame()
    {
        lose_PausedHUD.SetActive(false);

        timerText.text = "3";

        yield return new WaitForSeconds(1f);
        timerText.text = "2";

        yield return new WaitForSeconds(1f);
        timerText.text = "1";

        yield return new WaitForSeconds(1f);
        timerText.text = "";
        currentPrefab.GetComponent<PlayerSpaceBattle>().enabled = true;
        currentEnemy.GetComponent<SpaceBattleEnemy>().enabled = true;

    }

    public void EndGame()
    {
        Destroy(currentPrefab);

        lose_PausedHUD.SetActive(true);

        currentPrefab.GetComponent<PlayerSpaceBattle>().enabled = false;

        lose_PausedText.text = "You Lose";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        restart_Paused.onClick.AddListener(Start);
    }
}
