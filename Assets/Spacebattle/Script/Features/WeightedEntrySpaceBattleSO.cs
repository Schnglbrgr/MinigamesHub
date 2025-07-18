using UnityEngine;

[CreateAssetMenu(fileName = "WeightedEntrySO", menuName = "Scriptable Objects/WeightedEntrySO")]
public class WeightedEntrySpaceBattleSO : ScriptableObject
{
    public int weight;
    public GameObject prefab;
}
