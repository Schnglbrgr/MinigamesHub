using System.Collections.Generic;
using UnityEngine;

public class BallBouncePoolManager : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject Get(GameObject prefab, Vector2 position)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>();
        }
        Queue<GameObject> pool = pools[prefab];

        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(prefab, position, Quaternion.identity);
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
}
