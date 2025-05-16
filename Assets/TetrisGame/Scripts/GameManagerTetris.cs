using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private Button[] powerUps;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject selectPrefab;
    [SerializeField] private Button[] selectPlayer;
    [SerializeField] private GameObject slowCam;
    [SerializeField] private TMP_Text slowCamText;
    [SerializeField] private Transform holdPrefabSpawn;
    [SerializeField] private TMP_Text holdPrefabText;

    public int score;
    private int level;
    private float fallTime;
    private int heartsCount;
    private int prefabNum;
    private float invertoryChangePiece = 1f;
    private float invertorySelectPiece = 1f;
    public float invertoryBomb = 1f;
    private float invertorySlowMotion = 1f;
    public bool powerUpActive;
    private bool isHolding;

    public GameObject nextPrefab;
    private GameObject holdPrefab;
    private GameObject currentPrefab;
    public Transform[,] grid;
    public Vector2Int gridDimension = new Vector2Int(10, 20);

    private void Start()
    {
        score = 0;

        level = 1;

        grid = new Transform[gridDimension.x, gridDimension.y];

        currentPrefab = Instantiate(prefabs[0], spawnPoint.position, Quaternion.identity);

        NextPrefab();

        heartsCount = hearts.Length;

        fallTime = 0.5f;

        lose_PausedHUD.SetActive(false);

        PowerUps();

        isHolding = false;

        Time.timeScale = 1f;
    }

    private void Update()
    {

        scoreText.text = $"Score: {score}";
        levelText.text = $"Level: {level}";
        PausedGame();

        if (powerUpActive)
        {
            for (int x = 0; x < powerUps.Length; x++)
            {
                ChangeColorButton(x, Color.red);
            }
        }
        else
        {
            for (int x = 0; x < powerUps.Length; x++)
            {
                ChangeColorButton(x, Color.green);
            }
        }



        HoldPrefab();

    }

    public void SpawnNewBlock()
    {
        currentPrefab = Instantiate(nextPrefab, spawnPoint.position, Quaternion.identity);

        currentPrefab.GetComponent<PlayerTetris>().enabled = true;

        currentPrefab.GetComponent<PlayerTetris>().fallTime = fallTime;

        powerUpActive = false;
       

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

    public void CheckAndClearLines()
    {
        for (int y = 0; y < gridDimension.y; y++)
        {
            if (RowIsFull(y))
            {
                ClearRow(y);
                MoveRowDown(y);
                score += 20;
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

    public void MoveRowDown(int height)
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
            currentPrefab.GetComponent<PlayerTetris>().enabled = false;

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
        currentPrefab.GetComponent<PlayerTetris>().enabled = true;

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
            fallTime = Mathf.Max(fallTime -= 0.05f, 0);

            invertoryBomb = Mathf.Max(invertoryBomb++, 3);
            invertoryChangePiece = Mathf.Max(invertoryBomb++, 3);
            invertorySelectPiece = Mathf.Max(invertoryBomb++, 3);
            invertorySlowMotion = Mathf.Max(invertoryBomb++, 3);
        }

    }

    void PowerUps()
    {       
        powerUps[0].onClick.AddListener(() => ChangePiece(5));
        powerUps[1].onClick.AddListener(() => SelectPiece(10));
        powerUps[2].onClick.AddListener(() => Bomb());
        powerUps[3].onClick.AddListener(() => SlowCam(7));

    }

    void ChangePiece(int coolDown)
    {

        if (!powerUpActive && invertoryChangePiece >= 1)
        {
            ChangeColorButton(0, Color.red);
            Destroy(currentPrefab);
            Destroy(nextPrefab);
            SpawnNewBlock();
            NextPrefab();
            powerUpActive = true;
            invertoryChangePiece--;
        }

    }

    void SlowCam(int coolDown)
    {

        if (!powerUpActive && invertorySlowMotion >= 1)
        {
            ChangeColorButton(3, Color.red);
            slowCam.SetActive(true);
            slowCamText.text = "SlowMotion Active";

            currentPrefab.GetComponent<PlayerTetris>().fallTime = 1f;
            powerUpActive = true;
            invertorySlowMotion--;

            StartCoroutine(ReturnSpeed(coolDown));
        }
       
    }

    void Bomb()
    {
        if (!powerUpActive && invertoryBomb >= 1)
        {
            ChangeColorButton(2, Color.red);
            powerUpActive = true;
            Destroy(currentPrefab);
            invertoryBomb--;
            Instantiate(bomb, spawnPoint.position, Quaternion.identity);
        }                 
    }

    void SelectPiece(int coolDown)
    {

        if (invertorySelectPiece >= 1 && !powerUpActive)
        {
            ChangeColorButton(1, Color.red);

            Destroy(currentPrefab);

            selectPrefab.SetActive(true);

            invertorySelectPiece--;

            for (int x = 0; x < selectPlayer.Length; x++)
            {
                selectPlayer[x].gameObject.SetActive(true);

            }

            selectPlayer[0].onClick.AddListener(() => SelectPieceSpawn(0));
            selectPlayer[1].onClick.AddListener(() => SelectPieceSpawn(1));
            selectPlayer[2].onClick.AddListener(() => SelectPieceSpawn(2));
            selectPlayer[3].onClick.AddListener(() => SelectPieceSpawn(3));
            selectPlayer[4].onClick.AddListener(() => SelectPieceSpawn(4));
            selectPlayer[5].onClick.AddListener(() => SelectPieceSpawn(5));
            selectPlayer[6].onClick.AddListener(() => SelectPieceSpawn(6));
            selectPlayer[7].onClick.AddListener(() => SelectPieceSpawn(7));
        }
    }

    void SelectPieceSpawn(int prefabNum)
    {
        ChangeColorButton(1, Color.green);


        Instantiate(prefabs[prefabNum], spawnPoint.position, Quaternion.identity);

        selectPrefab.SetActive(false);

        for (int x = 0; x < selectPlayer.Length; x++)
        {
            selectPlayer[x].gameObject.SetActive(false);
        }

        powerUpActive = false;
    }

    private void ChangeColorButton(int num, Color color)
    {
        powerUps[num].GetComponent<Image>().color = color;
    }

    IEnumerator ReturnSpeed(int coolDown)
    {
        yield return new WaitForSeconds(4f);
        currentPrefab.GetComponent<PlayerTetris>().fallTime = fallTime;
        slowCam.SetActive(false);
        slowCamText.text = "";
        powerUpActive = false;
    }

    void HoldPrefab()
    {
        if (!isHolding && Input.GetKey(KeyCode.Q))
        {
            isHolding = true;

            holdPrefab = Instantiate(currentPrefab, holdPrefabSpawn.position, Quaternion.identity);

            holdPrefab.GetComponent<PlayerTetris>().enabled = false;

            Destroy(currentPrefab);

            SpawnNewBlock();

            Destroy(nextPrefab);

            NextPrefab();

            holdPrefabText.text = "Use Block: E";

        }

        if (isHolding && Input.GetKey(KeyCode.E))
        {
            isHolding = false;

            Destroy(currentPrefab);

            Destroy(holdPrefab);

            currentPrefab = Instantiate(holdPrefab,spawnPoint.position,Quaternion.identity);

            currentPrefab.GetComponent<PlayerTetris>().enabled = true;

            holdPrefabText.text = "Hold Block: Q";

        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lost";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        restart_Paused.onClick.AddListener(Start);

    }

}
