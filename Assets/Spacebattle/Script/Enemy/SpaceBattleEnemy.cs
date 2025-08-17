using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour, IDamageableSpaceBattle
{
    public SpaceBattleEnemySO enemy;
    public WeightedEntrySpaceBattleSO enemyEntry;
    private SpaceBattleManager spaceBattleManager;
    private GameObject prefab;
    private Animation animationEnemy;
    private UltimateAttackSpaceBattle ultimatePlayer;

    private int currentHealth;
    private int ultimateReward;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        ultimatePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateAttackSpaceBattle>();

        currentHealth = enemy.health;

        prefab = enemyEntry.prefab;

        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();

        animationEnemy = GetComponent<Animation>();

        ultimateReward = enemy.ultimateReward;
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

            if (ultimatePlayer.ultimateCharge < 100)
            {
                ultimatePlayer.ultimateCharge += ultimateReward;
            }
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
        IDamageableSpaceBattle isDamageable = collision.gameObject.GetComponent<IDamageableSpaceBattle>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(enemy.damage);

            spaceBattleManager.SpawnEnemies();

            spaceBattleManager.poolManager.Return(prefab, gameObject);
        }
    }

}



