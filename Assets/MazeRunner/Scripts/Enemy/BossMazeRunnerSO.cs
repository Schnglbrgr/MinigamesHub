using UnityEngine;

[CreateAssetMenu(fileName = "BossMazeRunnerSO", menuName = "Scriptable Objects/BossMazeRunnerSO")]
public class BossMazeRunnerSO : ScriptableObject
{
    public int health;
    public int damage;
    public float speed;
    public int manaReward;
    public float fireRate;
    public float spawnRate;
    public PickRandomItemSO pickRandomEnemy;
    public PickRandomItemSO pickRandomItem;
    public PickRandomItemSO pickRandomWeapon;

    public void SpawnEnemies(Transform[] spawns, GameObject gameManager)
    {
        for (int x = 0; x < spawns.Length; x++)
        {
            GameObject currentEnemy = gameManager.GetComponent<PoolManager>().PoolInstance(pickRandomEnemy.SelectRandomObject());

            currentEnemy.transform.position = spawns[x].position;
        }
    }

    public void SpawnItem(Transform spawn)
    {
        Instantiate(pickRandomItem.SelectRandomObject(), spawn.position, Quaternion.identity);

        Instantiate(pickRandomWeapon.SelectRandomObject(), spawn.position, Quaternion.identity);
    }
}
