using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeightedPickerTowerGameSO", menuName = "Scriptable Objects/WeightedPickerTowerGameSO")]
public class WeightedPickerTowerGameSO : ScriptableObject
{
    public List<WeightedEntryTowerGameSO> gameObject;

    private int cumulativeWeight;
    private int randomNum;
    private int totalWeight;

    public GameObject SelectRandomObject()
    {
        totalWeight = 0;

        for (int x = 0; x < gameObject.Count; x++)
        {
            totalWeight += gameObject[x].weight;
        }

        randomNum = Random.Range(0, totalWeight);

        cumulativeWeight = 0;

        for (int x = 0; x < gameObject.Count; x++)
        {
            cumulativeWeight += gameObject[x].weight;

            if (randomNum < cumulativeWeight)
            {
                return gameObject[x].prefab;
            }
        }
        return null;
    }
}
