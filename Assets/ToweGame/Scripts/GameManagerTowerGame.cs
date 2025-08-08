using UnityEngine;

public class GameManagerTowerGame : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawns;

    public WeightedPickerTowerGameSO enemyPicker;
    public PoolManagerTowerGame poolManager;

    public GameObject currentEnemy;

    private int randomSpawn;

    private void Awake()
    {
        poolManager = GetComponent<PoolManagerTowerGame>();
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentEnemy == null)
        {
            randomSpawn = Random.Range(0, enemySpawns.Length);

            currentEnemy = poolManager.CreateObject(enemyPicker.SelectRandomObject());

            currentEnemy.transform.position = enemySpawns[randomSpawn].position;
        }       
    }
}
