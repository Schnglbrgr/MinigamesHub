using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManagerTetris : MonoBehaviour
{
    [Header ("----Components----")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private TMP_Text lose_PausedText;
    [SerializeField] private Button restart_Paused;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject lose_PausedHUD;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Transform nextPrefabSpawn;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject slowCam;
    [SerializeField] private TMP_Text slowCamText;
    [SerializeField] private Transform holdPrefabSpawn;
    [SerializeField] private TMP_Text holdPrefabText;

    public GameObject nextPrefab;
    private GameObject holdPrefab;
    private GameObject currentPrefab;
    public Transform[,] grid;
    private AudioControllerTetris audioController;
    private PlayerInput playerInput;


    [Header("----Variables----")]
    public int score;
    private int level;
    private float fallTime;
    private int heartsCount;
    private int prefabNum;
    public bool powerUpActive;
    private bool isHolding;
    public Vector2Int gridDimension = new Vector2Int(10, 20);
    private float timer = 0f;
    private float cooldown = 3f;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerTetris>();

        playerInput = GetComponent<PlayerInput>();
    }

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

        isHolding = false;

        holdPrefabText.text = "Hold Block: Q";

        Time.timeScale = 1f;

    }

    private void Update()
    {

        scoreText.text = $"Score: {score}";

        levelText.text = $"Level: {level}";

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        CheckAndClearLines();
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

    //===============================================================
    private void CheckAndClearLines()
    {
        for (int y = 0; y < gridDimension.y; y++)
        {
            if (RowIsFull(y))
            {
                ClearRow(y);

                MoveRowDown(y);

                score += 20;

                NextLevel();

                audioController.MakeSound(audioController.destroyRow);
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

    //===============================================================

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

        audioController.MakeSound(audioController.loseHeart);
    }

    void NextLevel()
    {
        if (score % 100 == 0)
        {
            level += 1;

            fallTime = Mathf.Max(fallTime -= 0.05f, 0);

            audioController.MakeSound(audioController.levelUp);
        }

    }

    public void HoldPrefab(InputAction.CallbackContext context)
    {
        if (!isHolding && context.performed && timer <= 0)
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
    }

    public void UsePrefab(InputAction.CallbackContext context)
    {
        if (isHolding && context.performed)
        {
            isHolding = false;

            Destroy(currentPrefab);

            Destroy(holdPrefab);

            currentPrefab = Instantiate(holdPrefab, spawnPoint.position, Quaternion.identity);

            currentPrefab.GetComponent<PlayerTetris>().enabled = true;

            holdPrefabText.text = "Hold Block: Q";

            timer = cooldown;
        }
    }

    //===============================================================
    public void ChangePiece(InputAction.CallbackContext context)
    {

        if (!powerUpActive && context.performed)
        {
            Destroy(currentPrefab);

            Destroy(nextPrefab);

            SpawnNewBlock();

            NextPrefab();

            powerUpActive = true;

        }
    }

    public void SlowCam(InputAction.CallbackContext context)
    {

        if (!powerUpActive && context.performed)
        {
            slowCam.SetActive(true);

            slowCamText.text = "SlowMotion Active";

            currentPrefab.GetComponent<PlayerTetris>().fallTime = 1f;

            powerUpActive = true;

            StartCoroutine(ReturnSpeed());
        }   
    }

    public void Bomb(InputAction.CallbackContext context)
    {
        if (!powerUpActive && context.performed)
        {
            powerUpActive = true;

            Destroy(currentPrefab);

            Instantiate(bomb, spawnPoint.position, Quaternion.identity);
        }                 
    }

    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(4f);

        currentPrefab.GetComponent<PlayerTetris>().fallTime = fallTime;

        slowCam.SetActive(false);

        slowCamText.text = "";

        powerUpActive = false;
    }

    //===============================================================
    public void PausedGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentPrefab.GetComponent<PlayerTetris>().enabled = false;

            lose_PausedHUD.SetActive(true);

            lose_PausedText.text = "Game Paused";

            restart_Paused.GetComponentInChildren<TMP_Text>().text = "Resume";

            playerInput.SwitchCurrentActionMap("Pause");

            EventSystem.current.SetSelectedGameObject(restart_Paused.gameObject);
        }
    }

    public void ResumeGame()
    {
        lose_PausedHUD.SetActive(false);

        currentPrefab.GetComponent<PlayerTetris>().enabled = true;

        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    void EndGame()
    {
        Time.timeScale = 0f;

        lose_PausedHUD.SetActive(true);

        lose_PausedText.text = "You Lost";

        restart_Paused.GetComponentInChildren<TMP_Text>().text = "Restart";

        restart_Paused.onClick.AddListener(Start);

        audioController.MakeSound(audioController.gameOver);

        EventSystem.current.SetSelectedGameObject(restart_Paused.gameObject);
    }

}
