using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lostMessage;
    [SerializeField] private GameObject hud; 
        
    private Button[] buttons;

    private Transform[,] grid;
    private int score;
    private Vector2Int gridDimension;
    public bool IsLand;

    private void Awake()
    {
        gridDimension = new Vector2Int(20,30);
    }

    private void Start()
    {
        score = 0;

        grid = new Transform[gridDimension.x, gridDimension.y];

        SpawnNewBlock();
    }

    void SpawnNewBlock()
    {
        int selectPrefab = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[selectPrefab], spawnPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        if (IsLand)
        {
            CheckAndClearLines();
            SpawnNewBlock();
        }
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
                    RowDown(y);
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
            Destroy(grid[x, height].gameObject);
            grid[x, height] = null;
        }
    }

    void RowDown(int height)
    {
       
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

    public bool IsValid(Transform player)
    {
        foreach (Transform block in player)
        {
            Vector2Int position = Vector2Int.RoundToInt(player.position);

            if (!InsideMap(position) || grid[position.x, position.y] != null)
            {
                return false;
            }
        }

        return true;
    }

    bool InsideMap(Vector2Int position)
    {
        if (position.x >= 0 && position.x < gridDimension.x && position.y >= 0 && position.y < gridDimension.y)
        {
            return true;
        }
        return false;
    }

    void Engame()
    {
        hud.SetActive(true);

        lostMessage.text = $"You Lost {score}";  
        
        buttons[0].onClick.AddListener(Start);

        //buttons[1].onClick.AddListener(Exit);
    }
}
