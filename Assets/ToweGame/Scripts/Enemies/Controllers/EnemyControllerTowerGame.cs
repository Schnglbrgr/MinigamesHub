using UnityEngine;

public abstract class EnemyControllerTowerGame : MonoBehaviour, IDamageableTowerGame
{
    public EnemyControllerSOTowerGame enemyController;
    public PoolManagerTowerGame poolManager;

    public int health;
    public int damage;
    public float speed;
    public int currentHealth;

    public abstract void Movement();

    public abstract void Attack();

    public abstract void CheckHealth();

    public virtual void TakeDamage(int damage)
    {
    }
}
