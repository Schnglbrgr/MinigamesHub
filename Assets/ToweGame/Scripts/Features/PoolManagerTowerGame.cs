using System.Collections.Generic;
using UnityEngine;

public class PoolManagerTowerGame : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

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
            GameObject newObj = Instantiate(prefab);
            return newObj;
        }
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
}
