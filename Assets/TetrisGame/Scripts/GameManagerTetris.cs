using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTetris : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private Button start;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject HUD;
    [SerializeField] private TMP_Text scoreText;

    private int score;
    public bool isLand;

    private Transform[,] grid;
    private Vector2Int gridDimension;

    private void Awake()
    {
        gridDimension = new Vector2Int(10,20);
    }

    private void Start()
    {
        score = 0;
        grid = new Transform[gridDimension.x, gridDimension.y];
        SpawnNewBlock();
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        if (isLand)
        {
            SpawnNewBlock();
            CheckAndClearLines();           
        }
    }

    void SpawnNewBlock()
    {
        int currentPrefab = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[currentPrefab], spawnPoint.position, Quaternion.identity);

        isLand = false;
    }

    void CheckAndClearLines()
    {
        for (int y = 0; y < gridDimension.y; y++)
        {
            for (int x = 0; x < gridDimension.x; x++)
            {
                if (RowIsFull(y))
                {
                    ClearRow(y);
                    MoveRowDown(y);
                    score += 10;
                }
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
            Vector2Int position = Vector2Int.RoundToInt(player.position);

            if (InsideMap(position))
            {
                grid[position.x, position.y] = block;
            }
        }
    }

    public bool IsValidMove(Transform player)
    {
        foreach (Transform block in player)
        {
            Vector2Int position = Vector2Int.RoundToInt(player.position);

            if (!InsideMap(player.position) || grid[position.x,position.y] != null)
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

    void EndGame()
    {
        HUD.SetActive(true);

        loseText.text = $"You Lost: {score}";

        start.onClick.AddListener(Start);

    }

}
