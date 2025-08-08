using UnityEngine;

public class GameManagerTowerGame : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawns;

    public PoolManagerTowerGameSO poolManager;
    public WeightedPickerTowerGameSO enemyPicker;

    private GameObject currentEnemy;

    private int randomSpawn;

    private void SpawnEnemy()
    {
        randomSpawn = Random.Range(0, enemySpawns.Length);

        currentEnemy = poolManager.CreateObject(enemyPicker.SelectRandomObject());

        currentEnemy.transform.position = enemySpawns[randomSpawn].position;
    }
}
