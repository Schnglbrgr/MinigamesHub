using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMazeRunnerSO", menuName = "Scriptable Objects/EnemyMazeRunnerSO")]
public class EnemyMazeRunnerSO : ScriptableObject
{
    public string nameEnemy;
    public int weight;
    public int health;
    public int damage;
    public int mana;
    public GameObject prefab;

    
}
