using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PoolManagerSO", menuName = "Scriptable Objects/PoolManagerSO")]

public class PoolManagerSO : ScriptableObject
{
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    private int randomSpawn;
    private Vector3 randomSpawnPosition;

    public GameObject CreateObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>();
        }

        Queue<GameObject> pool = pools[prefab];

        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Instantiate(prefab);
        }
        return null;
    }

    public void Return(GameObject prefab, GameObject currentObject)
    {
        currentObject.SetActive(false);

        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>();
        }
        pools[prefab].Enqueue(currentObject);
    }
    public Vector3 PickRandomSpawn()
    {
        randomSpawn = Random.Range(2, 8);

        randomSpawnPosition = new Vector3(randomSpawn, 17f, -1f);

        return randomSpawnPosition;
    }
}
