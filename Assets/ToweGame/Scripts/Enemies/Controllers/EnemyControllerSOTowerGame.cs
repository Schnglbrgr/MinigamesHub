using UnityEngine;

[CreateAssetMenu(fileName = "EnemyControllerSOTowerGame", menuName = "Scriptable Objects/EnemyControllerSOTowerGame")]
public class EnemyControllerSOTowerGame : ScriptableObject
{
    public int health;
    public int damage;
    public float speed;
    public GameObject prefab;
}
