using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PickRandom", menuName = "Scriptable Objects/PickRandom")]
public class WeightedPickerSO : ScriptableObject
{
    public List<SpaceBattleEnemySO> objects;

    private int totalWeight;
    private int pickRandomNum;
    private int cumulativeWeight;

    public GameObject GetRandomObject()
    {
        totalWeight = 0;

        for (int x = 0; x < objects.Count; x++)
        {
            totalWeight += objects[x].weight;
        }

        pickRandomNum = Random.Range(0, totalWeight);
        cumulativeWeight = 0;

        for (int x = 0; x < objects.Count; x++)
        {
            cumulativeWeight += objects[x].weight;

            if (pickRandomNum < cumulativeWeight)
            {
                return objects[x].prefab;
            }
        }
        return null;
    }
}
