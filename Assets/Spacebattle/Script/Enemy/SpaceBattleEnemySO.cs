using UnityEngine;

[CreateAssetMenu(fileName = "SpaceBattleEnemySO", menuName = "Scriptable Objects/SpaceBattleEnemySO")]
public class SpaceBattleEnemySO : ScriptableObject
{
    public string nameEnemy;
    public int weight;
    public float speed;
    public int health;
    public int damage;
    public int childNum;
    public GameObject prefab;
}
