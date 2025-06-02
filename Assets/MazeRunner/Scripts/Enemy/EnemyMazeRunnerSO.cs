using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMazeRunnerSO", menuName = "Scriptable Objects/EnemyMazeRunnerSO")]
public class EnemyMazeRunnerSO : ScriptableObject
{
    public string nameEnemy;
    public int health;
    public int damage;
    public int mana;
    public float speed;
    
}
