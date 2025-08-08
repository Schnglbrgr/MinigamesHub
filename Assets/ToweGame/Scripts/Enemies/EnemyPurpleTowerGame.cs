using UnityEngine;

public class EnemyPurpleTowerGame : EnemyControllerTowerGame
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

    }

    public override void CheckHealth()
    {

    }
}
