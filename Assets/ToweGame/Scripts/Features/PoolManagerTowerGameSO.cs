using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerTowerGameSO", menuName = "Scriptable Objects/PoolManagerTowerGameSO")]
public class PoolManagerTowerGameSO : ScriptableObject
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
}
