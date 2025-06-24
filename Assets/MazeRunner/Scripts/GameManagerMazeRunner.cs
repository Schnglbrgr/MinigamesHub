using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMazeRunner : MonoBehaviour
{
    [SerializeField] private GameObject win_loseHUD;
    [SerializeField] private TMP_Text win_loseText;
    [SerializeField] private Button restart;
    [SerializeField] private Button exit;
    [SerializeField] private Transform[] chestSpawn;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] chests;
    [SerializeField] private Transform[] enemySpawn;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private Transform chestParent;
    [SerializeField] private Transform enemySpawnParent;
    [SerializeField] private PickRandomItemSO pickRandomEnemy;
    [SerializeField] private PickRandomItemSO pickRandomBoss;
    [SerializeField] private GameObject arrow;

    public GameObject[] teleports;
    public Transform bossSpawn;
    public GameObject warningMessage;
    public TMP_Text maxHealthShield;
    public GameObject currentBoss;
    public PlayerController player;
    private Animation openDoor;
    private AudioControllerMazeRunner audioController;

    public int deathBoss;
    public bool bossActive = false;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        for (int x = 0; x < weapons.Length; x++)
        {
            weapons[x].GetComponent<AttackSystem>().ammoHUD = GameObject.FindGameObjectWithTag("Ammo");
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        openDoor = GameObject.FindGameObjectWithTag("Wall").GetComponent<Animation>();
    }

    private void Start()
    {
        SpawnRandonEnemies();
        SpawnChest();
    }

    private void SpawnChest()
    {
        for (int x = 0; x < chests.Length; x++)
        {
            int randomPosition = Random.Range(0, chestSpawn.Length);

            Instantiate(chests[x], chestSpawn[randomPosition].position, Quaternion.identity, chestParent);
        }
    }

    public void StartBoss()
    {
        if (player.keyInventory == 10)
        {
            openDoor.Play();

            bossActive = true;

            arrow.SetActive(true);

            Invoke("SpawnBoss", 30);

            player.keyInventory = 0;

            audioController.musicSound.clip = audioController.finalBoss;

            audioController.musicSound.Play();

        }
        else if (player.keyInventory < 10)
        {
            warningMessage.SetActive(true);
        }

    }

    private void SpawnBoss()
    {
        currentBoss = pickRandomBoss.SelectRandomObject();

        GetComponent<PoolManager>().PoolInstance(currentBoss);

    }

    public void Win()
    {
        if (deathBoss >= 2)
        {
            win_loseHUD.SetActive(true);

            win_loseText.text = "You Win!";

            win_loseText.color = Color.green;

            audioController.MakeSound(audioController.win);

            restart.onClick.AddListener(Restart);

            Time.timeScale = 0f;
        }
    }

    private void Restart()
    {
        //
    }

    public void Lose()
    {

        win_loseHUD.SetActive(true);

        win_loseText.text = "You Lose!";

        win_loseText.color = Color.red;

        audioController.MakeSound(audioController.gameOver);

        Time.timeScale = 0f;

    }

    private void SpawnRandonEnemies()
    {
        for (int x = 0; x < enemySpawn.Length; x++)
        {
           Instantiate(pickRandomEnemy.SelectRandomObject(), enemySpawn[x].position, Quaternion.identity, enemySpawnParent);
        }
    }

}
