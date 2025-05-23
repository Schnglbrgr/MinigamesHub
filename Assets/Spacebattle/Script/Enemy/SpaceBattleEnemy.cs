using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleEnemy : MonoBehaviour
{
    [SerializeField] private SpaceBattleEnemySO enemy;

    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;
    private PoolQueue poolQueue;

    private int health;
    private int currentHealth;
    private int childNumber;
    private Color colorEnemy;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        poolQueue = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolQueue>();

        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();

        health = enemy.health;

        currentHealth = health;

        colorEnemy = GetComponentInChildren<SpriteRenderer>().color;

        childNumber = enemy.childNumber;
    }

    private void FixedUpdate()
    {
        Movement();
        ReachedFloor();
    }

    private void Update()
    {
        CheckHealth();

    }

    void Movement()
    {
        transform.position += Vector3.down * enemy.speed * Time.deltaTime;
    }

    void ReachedFloor()
    {
        if (transform.position.y <= 0)
        {
            //poolQueue.Return(gameObject, spaceBattleManager.currentEnemy);
            spaceBattleManager.EndGame();           
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        for (int x = 0; x < childNumber; x++)
        {
           gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color = Color.red;
           StartCoroutine(ReturnColor(x));
        }
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            spaceBattleManager.SpawnEnemies();
            spaceBattleManager.score += 10;
            //poolQueue.Return(gameObject, spaceBattleManager.currentEnemy);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spaceBattleManager.LevelUp(enemy.speed);

        if (collision.gameObject.tag == "Player")
        {
            healthSpaceBattle.TakeDamage(enemy.damage);
            //poolQueue.Return(gameObject, spaceBattleManager.currentEnemy);
        }
    }

    IEnumerator ReturnColor(int x)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color = colorEnemy;
    }


}
