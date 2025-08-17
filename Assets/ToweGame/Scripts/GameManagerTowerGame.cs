using UnityEngine;

public class GameManagerTowerGame : MonoBehaviour
{
    public WeightedPickerTowerGameSO enemyPicker;
    public PoolManagerTowerGame poolManager;
    public GameObject currentEnemy;

    private void Awake()
    {
        poolManager = GetComponent<PoolManagerTowerGame>();

        currentEnemy = poolManager.CreateObject(enemyPicker.SelectRandomObject());
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentEnemy == null)
        {
            currentEnemy = poolManager.CreateObject(enemyPicker.SelectRandomObject());
        }       
    }
}
