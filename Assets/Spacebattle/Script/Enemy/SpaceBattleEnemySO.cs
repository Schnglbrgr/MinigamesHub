using UnityEngine;

[CreateAssetMenu(fileName = "SpaceBattleEnemySO", menuName = "Scriptable Objects/SpaceBattleEnemySO")]
public class SpaceBattleEnemySO : ScriptableObject
{
    public string nameEnemy;
    public float speed;
    public int health;
    public int damage;
}
