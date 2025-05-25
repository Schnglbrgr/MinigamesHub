using UnityEngine;

[CreateAssetMenu(fileName = "HelpsItems", menuName = "Scriptable Objects/HelpsItems")]
public class HelpsItemsSO : ScriptableObject
{
    public string nameItem;
    public int weight;
    public GameObject prefab;
}
