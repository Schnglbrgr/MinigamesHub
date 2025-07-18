using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncePoolManager : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
    private float spawnHeight = 6f;

    public GameObject Get(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>();
        }
        Queue<GameObject> pool = pools[prefab];

        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            SpawnPosition(obj);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            float spawnX = 7.5f;
            float randomX = Random.Range(-spawnX, spawnX);

            return Instantiate(prefab, SpawnPosition(prefab), Quaternion.identity);
        }
    }

    public void Return(GameObject prefab, GameObject instance)
    {
        instance.SetActive(false);
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>();
        }
        pools[prefab].Enqueue(instance);
    }

    private Vector2 SpawnPosition(GameObject obj)
    {
        float spawnX = 7.5f;
        float randomX = Random.Range(-spawnX, spawnX);

        return obj.transform.position = new Vector2(randomX, spawnHeight);
    }
}
