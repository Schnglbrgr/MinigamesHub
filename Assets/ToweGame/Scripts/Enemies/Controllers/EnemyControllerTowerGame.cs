using UnityEngine;

public abstract class EnemyControllerTowerGame : MonoBehaviour, IDamageableTowerGame
{
    public EnemyControllerSOTowerGame enemyController;
    public PoolManagerTowerGame poolManager;
    public Vector3 randomPosition;

    public int health;
    public int damage;
    public float speed;
    public int currentHealth;
    public int xValue;
    public int zValue;

    public abstract void Movement();

    public abstract void Attack();

    public abstract void CheckHealth();

    public virtual void TakeDamage(int damage)
    {
    }

    public Vector3 RandomPosition()
    {
        xValue = Random.Range(-500, 400);
        zValue = Random.Range(-700, -150);

        randomPosition = new Vector3(xValue, 12f, zValue);

        return randomPosition;
    }
}
