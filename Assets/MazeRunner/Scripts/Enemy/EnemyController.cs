using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMazeRunnerSO enemy;

    private ManaSystem manaSystem;

    public int currentHealth;
    public int damage;
    private int manaReward;

    private void Awake()
    {
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

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

}
