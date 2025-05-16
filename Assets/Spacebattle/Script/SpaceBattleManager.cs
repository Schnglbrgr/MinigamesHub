using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] enemiesSpawnPoint;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private TMP_Text timerText;

    private Vector2Int map = new Vector2Int(10,20);
    private GameObject currentEnemy;
    private GameObject currentPrefab;
    private int randomSpawn;
    private int randomEnemy;
    public int score;

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
        lose_PausedHUD.SetActive(false);
        score = 0;
        scoreText.text = $"Score: {score}";
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        PausedGame();
    }

    void SpawnPlayer()
    {
        currentPrefab = Instantiate(player, spawnPoint.position, Quaternion.identity);
    }

    public void SpawnEnemies()
    {
        randomSpawn = Random.Range(0, enemiesSpawnPoint.Length);
        randomEnemy = Random.Range(0, enemies.Length);

        currentEnemy = Instantiate(enemies[randomEnemy], enemiesSpawnPoint[randomSpawn].position ,Quaternion.identity);
    }


    public bool InsideMap(Transform player)
    {
        if (player.position.x >= 0 && player.position.x < map.x && player.position.y >= 0 && player.position.x < map.y)
        {
            return true;
        }

        return false;
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
