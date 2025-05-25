using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeightedEntrySO", menuName = "Scriptable Objects/WeightedEntrySO")]
public class WeightedEntrySO : ScriptableObject
{
    public List<ScriptableObject> gameObjects;

    public int weight;
    public GameObject prefab;

}
