using UnityEngine;

public class EnemyMedium : EnemyController
{
    [SerializeField] private EnemyMazeRunnerSO enemy;

    private int currentHealth;
    public int damage;
    private int manaReward;

    private ManaSystem manaSystem;
    private GameObject player;
    private float speed = 1.5f;

    private void Awake()
    {
        currentHealth = enemy.health;
        damage = enemy.damage;
        manaReward = enemy.mana;

        player = GameObject.FindGameObjectWithTag("Player");
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void Movement()
    {
        if (CheckPositionPlayer())
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public override void Attack()
    {  
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CheckHealth();
        GetComponent<Animation>().Play();
    }

    public override void CheckHealth()
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
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(damage);
        }
    }

    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 4.5f)
        {
            return true;
        }
        return false;
    }

}
