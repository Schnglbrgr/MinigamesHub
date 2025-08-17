using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeightedPickerSO", menuName = "Scriptable Objects/WeightedPickerSO")]

public class WeightedPickerSpaceBattleSO : ScriptableObject
{
    public List<WeightedEntrySpaceBattleSO> enemies;

    private int totalWeight;
    private int randomNum;
    private int cumulativeWeight;

    public GameObject SelectRandomObject()
    {
        totalWeight = 0;

        for (int x = 0; x < enemies.Count; x++)
        {
            totalWeight += enemies[x].weight;
        }

        randomNum = Random.Range(0, totalWeight);
        cumulativeWeight = 0;

        for (int x = 0; x < enemies.Count; x++)
        {
            cumulativeWeight += enemies[x].weight;

            if (randomNum < cumulativeWeight)
            {
                return enemies[x].prefab;
            }
        }
        return null;
    }
}
