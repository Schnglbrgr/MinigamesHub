using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMazeRunner : MonoBehaviour
{
    [SerializeField] private GameObject win_loseHUD;
    [SerializeField] private TMP_Text win_loseText;
    [SerializeField] private Button restart;
    [SerializeField] private Button exit;
    [SerializeField] private Transform[] weaponsSpawn;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform[] enemySpawn;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private Transform weaponSpawnParent;
    [SerializeField] private Transform enemySpawnParent;

    public GameObject warningMessage;

    private void Awake()
    {
        for (int x = 0; x < weapons.Length; x++)
        {
            weapons[x].GetComponent<AttackSystem>().ammoHUD = GameObject.FindGameObjectWithTag("Ammo");
        }
    }

    private void Start()
    {
        SpawnRandomWeapons();

        SpawnRandonEnemies();
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

    private void SpawnRandomWeapons()
    {
        for (int x = 0; x < weaponsSpawn.Length; x++)
        {
            int selectWeapon = Random.Range(0,weapons.Length);

            Instantiate(weapons[selectWeapon], weaponsSpawn[x].position,Quaternion.identity, weaponSpawnParent);

            weapons[selectWeapon].GetComponent<RotateWeapon>().enabled = false;
        }
    }

    private void SpawnRandonEnemies()
    {
        for (int x = 0; x < enemySpawn.Length; x++)
        {
            int selectEnemy = Random.Range(0, enemy.Length);

            Instantiate(enemy[selectEnemy], enemySpawn[x].position, Quaternion.identity, enemySpawnParent);

            enemy[selectEnemy].GetComponent<EnemyHealthSystem>().enabled = false;
        }
    }

}
