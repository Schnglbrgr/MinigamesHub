using UnityEngine;

[CreateAssetMenu(fileName = "SpaceBattleEnemySO", menuName = "Scriptable Objects/SpaceBattleEnemySO")]
public class SpaceBattleEnemySO : ScriptableObject
{
    public string nameEnemy;
    public int weight;
    public int health;
    public int damage;
    public float speed;
    public GameObject prefab;
    public int childNumber;
}
