using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTetris : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Transform nextPrefabSpawn;

    private int score;
    public int level;
    private int heartsCount;
    private int prefabNum;

    public GameObject nextPrefab;
    private GameObject currentPrefab;
    private Transform[,] grid;
    private Vector2Int gridDimension = new Vector2Int(10, 20);

    private void Start()
    {
        score = 90;
        level = 1;
        grid = new Transform[gridDimension.x, gridDimension.y];
        Instantiate(prefabs[0], spawnPoint.position, Quaternion.identity);
        NextPrefab();
        heartsCount = hearts.Length;
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";
        levelText.text = $"Level: {level}";
        PausedGame();

    }

    public void SpawnNewBlock()
    {
        currentPrefab = Instantiate(nextPrefab, spawnPoint.position, Quaternion.identity);

        currentPrefab.GetComponent<PlayerTetris>().enabled = true;

        if (!IsValidMove(currentPrefab.transform))
        {
            LoseHearts();
        }
    }

    public void NextPrefab()
    {

        prefabNum = Random.Range(0, prefabs.Length);

        nextPrefab = Instantiate(prefabs[prefabNum], nextPrefabSpawn.position, Quaternion.identity);

        nextPrefab.GetComponent<PlayerTetris>().enabled = false;
       
    }

    void CheckAndClearLines()
    {
        for (int y = 0; y < gridDimension.y; y++)
        {
            if (RowIsFull(y))
            {
                ClearRow(y);
                MoveRowDown(y);
                score += 10;
                NextLevel();
            }
        }
    }

    bool RowIsFull(int height)
    {
        for (int x = 0; x < gridDimension.x; x++)
        {
            if (grid[x,height] == null)
            {
                return false;
            }
        }

        return true;

    }

    void ClearRow(int height)
    {
        for (int x = 0; x < gridDimension.x; x++)
        {
            Destroy(grid[x,height].gameObject);
            grid[x, height] = null;
        }
    }

    void MoveRowDown(int height)
    {
        for (int y = height + 1; y < gridDimension.y; y++)
        {
            for (int x = 0; x < gridDimension.x; x++)
            {
                if (grid[x,y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
        
    }

    public void AddToGrid(Transform player)
    {
        foreach (Transform block in player)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);

            if (InsideMap(position))
            {
                grid[position.x, position.y] = block;
            }
        }

        CheckAndClearLines();
    }

    public bool IsValidMove(Transform player)
    {
        foreach (Transform block in player)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);

            if (!InsideMap(position) || grid[position.x,position.y] != null)
            {
                return false;
            }
        }

        return true;
    }

    bool InsideMap(Vector2 position)
    {
        if (position.x >= 0 && position.x < gridDimension.x && position.y >= 0 && position.y < gridDimension.y )
        {
            return true;
        }
        return false;
    }


    void PausedGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;

            lose_PausedHUD.SetActive(true);

            lose_PausedText.text = "Game Paused";

            restart_Paused.GetComponentInChildren<TMP_Text>().text = "Resume";

            restart_Paused.onClick.AddListener(ResumeGame);
        }
    }

    void ResumeGame()
    {
        lose_PausedHUD.SetActive(false);

        Time.timeScale = 1f;
    }

    void LoseHearts()
    {
        heartsCount--;

        for (int y = 0; y < gridDimension.y; y++)
        {
            for (int x = 0; x < gridDimension.x; x++)
            {
                if (grid[x,y] != null)
                {
                    Destroy(grid[x,y].gameObject);
                    grid[x, y] = null;
                }
            }
        }

        if (heartsCount <= 0)
        {
            EndGame();
        }

        Destroy(hearts[heartsCount]);
        Destroy(currentPrefab);
        score = Mathf.Max(score - 50, 0);
        level = Mathf.Max(level--, 1);

        SpawnNewBlock();
    }

    void NextLevel()
    {
        if (score % 100 == 0)
        {
            level += 1;
        }
       
    }

    void EndGame()
    {
        Time.timeScale = 0f;

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lost";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        //restart_Paused.onClick.AddListener();

    }

}
