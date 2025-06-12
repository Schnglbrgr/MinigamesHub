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
    [SerializeField] private PickRandomItemSO pickRandomItemSO;

    public GameObject warningMessage;
    public TMP_Text maxHealthShield;

    private void Awake()
    {
        for (int x = 0; x < weapons.Length; x++)
        {
            //weapons[x].GetComponent<AttackSystem>().ammoHUD = GameObject.FindGameObjectWithTag("Ammo");
        }
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

    public void Win(int keyInventory)
    {
        if (keyInventory == 10)
        {
            win_loseHUD.SetActive(true);

            win_loseText.text = "You Win!";

            win_loseText.color = Color.green;

            restart.onClick.AddListener(Restart);

            Time.timeScale = 0f;
        }
        else if (keyInventory < 10)
        {
            warningMessage.SetActive(true);
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

        Time.timeScale = 0f;

    }

    private void SpawnRandonEnemies()
    {
        for (int x = 0; x < enemySpawn.Length; x++)
        {
           Instantiate(pickRandomItemSO.SelectRandomObject(), enemySpawn[x].position, Quaternion.identity, enemySpawnParent);
        }
    }

}
