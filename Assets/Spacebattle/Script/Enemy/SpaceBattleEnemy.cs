using System.Collections;
using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour, IDamageable
{
    public SpaceBattleEnemySO enemy;
    public WeightedEntrySO enemyEntry;
    private SpaceBattleManager spaceBattleManager;
    private GameObject prefab;
    private Animation animationEnemy;

    private int currentHealth;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentHealth = enemy.health;

        prefab = enemyEntry.prefab;

        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();

        animationEnemy = GetComponent<Animation>();

    }


    private void OnDisable()
    {
        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();
        animationEnemy.Play();
        currentHealth = enemy.health;
    }

    private void FixedUpdate()
    {
        Movement();
        ReachedFloor();
    }

    void Movement()
    {
        transform.position += Vector3.down * enemy.speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animationEnemy.Play();

        CheckHealth();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            spaceBattleManager.score += 10;
            spaceBattleManager.SpawnEnemies();
            spaceBattleManager.LevelUp(enemy.speed);
            spaceBattleManager.poolManager.Return(prefab,gameObject);
        }
    }

    void ReachedFloor()
    {
        if (transform.position.y <= 0)
        {
            spaceBattleManager.poolManager.Return(prefab, gameObject);
            spaceBattleManager.EndGame();           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(enemy.damage);
            spaceBattleManager.SpawnEnemies();
            spaceBattleManager.poolManager.Return(prefab, gameObject);
        }
    }

}



