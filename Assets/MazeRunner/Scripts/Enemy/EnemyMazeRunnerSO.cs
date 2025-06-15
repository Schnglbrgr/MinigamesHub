using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMazeRunnerSO", menuName = "Scriptable Objects/EnemyMazeRunnerSO")]
public class EnemyMazeRunnerSO : ScriptableObject
{
    public string nameEnemy;
    public int health;
    public int damage;
    public int mana;
    public float speed;
    public int ammoReward;
    
    public void PushItems(Rigidbody2D rbItem, Vector2 direction, float pushForce)
    {
        rbItem.AddForce(direction * pushForce, ForceMode2D.Impulse);
    }

}
