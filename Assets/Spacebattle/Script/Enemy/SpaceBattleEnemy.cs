using System.Collections;
using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour
{
    public SpaceBattleEnemySO enemy;
    public WeightedEntrySO enemyEntry;
    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;
    private GameObject prefab;
    private Animation animationEnemy;

    private int currentHealth;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();

        currentHealth = enemy.health;

        prefab = enemyEntry.prefab;

        spaceBattleManager.poolManager.PickRandomSpawn();

        animationEnemy = GetComponent<Animation>();

    }


    private void OnDisable()
    {
        spaceBattleManager.poolManager.PickRandomSpawn();
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

        for (int x = 0; x < enemy.childNum; x++)
        {
            gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color = Color.red;
            animationEnemy.Play();
        }

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
        if (collision.gameObject.tag == "Player")
        {
            healthSpaceBattle.TakeDamage(enemy.damage);
            spaceBattleManager.poolManager.Return(prefab, gameObject);
        }
    }

}



