using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "PickRandomItemSO", menuName = "Scriptable Objects/PickRandomItemSO")]
public class PickRandomItemSO : ScriptableObject
{
    public List<WeightedEntrySO> items;

    private int totalWeight;
    private int randomNum;
    private int cumulativeWeight;

    public GameObject SelectRandomObject()
    {
        totalWeight = 0;

        for (int x = 0; x < items.Count; x++)
        {
            totalWeight += items[x].weight;
        }

        randomNum = Random.Range(0, totalWeight);
        cumulativeWeight = 0;

        for (int x = 0; x < items.Count; x++)
        {
            cumulativeWeight += items[x].weight;

            if (randomNum < cumulativeWeight)
            {
                return items[x].prefab;
            }
        }

        return null;
    }
}
