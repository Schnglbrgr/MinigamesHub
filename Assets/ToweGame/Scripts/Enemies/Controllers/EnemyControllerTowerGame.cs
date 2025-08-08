using UnityEngine;

public abstract class EnemyControllerTowerGame : MonoBehaviour, IDamageableTowerGame
{
    public EnemyControllerSOTowerGame enemyController;
    public PoolManagerTowerGameSO poolManager;

    public int health;
    public int damage;
    public float speed;

    public abstract void Movement();

    public abstract void Attack();

    public abstract void CheckHealth();

    public virtual void TakeDamage(int damage)
    {
    }
}
