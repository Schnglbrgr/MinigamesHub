using System.Collections;
using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour
{
    [SerializeField] private SpaceBattleEnemySO enemy;

    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;
    private Color currentColor;

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

    private void Update()
    {
        CheckHealth();
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
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            spaceBattleManager.score += 10;
            spaceBattleManager.SpawnEnemies();
            spaceBattleManager.LevelUp(enemy.speed);
            Destroy(gameObject);
        }
    }

    void ReachedFloor()
    {
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
            spaceBattleManager.EndGame();           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        

        if (collision.gameObject.tag == "Player")
        {
            healthSpaceBattle.TakeDamage(enemy.damage);
            Destroy(gameObject);
        }
    }

    IEnumerator ReturnColor(int x)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(x).GetComponent<SpriteRenderer>().color = currentColor;
    }
}



