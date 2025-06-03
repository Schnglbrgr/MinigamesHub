using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMazeRunnerSO enemy;

    private ManaSystem manaSystem;
    private HealthSystem playerHealthSystem;

    public int currentHealth;
    public int damage;
    private int manaReward;

    private void Awake()
    {
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();

        currentHealth = enemy.health;

        damage = enemy.damage;

        manaReward = enemy.mana;

    }


    public void TakeDamageEnemy(int damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            if (manaSystem.mana < 100)
            {
                manaSystem.mana += manaReward;
            }

            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthSystem.TakeDamage(damage);
        }
    }

}
