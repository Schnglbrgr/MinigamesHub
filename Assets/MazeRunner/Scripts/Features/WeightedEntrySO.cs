using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeightedEntrySO", menuName = "Scriptable Objects/WeightedEntrySO")]
public class WeightedEntrySO : ScriptableObject
{
    public int weight;
    public GameObject prefab;
}
