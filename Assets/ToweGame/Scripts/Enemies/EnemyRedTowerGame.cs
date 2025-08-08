using UnityEngine;

public class EnemyRedTowerGame : EnemyControllerTowerGame
{
    private void Awake()
    {
        damage = enemyController.damage;

        health = enemyController.health;

        speed = enemyController.speed;

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerTowerGame>().poolManager;

    }

    public override void Movement()
    {

    }

    public override void Attack()
    {

    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;

        CheckHealth();
    }

    public override void CheckHealth()
    {
        switch (currentHealth)
        {
            case <= 0:
                poolManager.Return(enemyController.prefab, gameObject);
            break;

            case < 40:
                damage *= 2;
                speed *= 2;
            break;
        }
    }
}
