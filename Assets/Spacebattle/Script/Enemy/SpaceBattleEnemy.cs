using System.Collections;
using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour
{
    public SpaceBattleEnemySO enemy;
    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;
    private Color currentColor;
    public GameObject prefab;

    private int currentHealth;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();

        currentHealth = enemy.health;

        for (int x = 0; x < enemy.childNum; x++)
        {
            currentColor = gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color;
        }
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
            StartCoroutine(ReturnColor(x));
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
            spaceBattleManager.enemyPool.Return(prefab,gameObject);
        }
    }

    void ReachedFloor()
    {
        if (transform.position.y <= 0)
        {
            spaceBattleManager.enemyPool.Return(prefab, gameObject);
            spaceBattleManager.EndGame();           
        }
    }

    public void ResetState()
    {
        //currentHealth = enemy.health;
        currentHealth = 100;
        prefab = spaceBattleManager.currentPrefabEnemy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        

        if (collision.gameObject.tag == "Player")
        {
            healthSpaceBattle.TakeDamage(enemy.damage);
            spaceBattleManager.enemyPool.Return(prefab, gameObject);
        }
    }

    IEnumerator ReturnColor(int x)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color = currentColor;
    }
}



