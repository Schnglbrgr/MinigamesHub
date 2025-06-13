using UnityEngine;

public class EnemyLight : EnemyController
{
    [SerializeField] private EnemyMazeRunnerSO enemy;
    [SerializeField] private Transform[] patrolsPoints;
    [SerializeField] private AttackEnemyLight attack;

    private ManaSystem manaSystem;
    private Transform wayParent;
    private float speed = 1f;
    private int randomNum;

    private int currentHealth;
    private int damage;
    private int manaReward;


    private void Awake()
    {
        wayParent = GameObject.FindGameObjectWithTag("Ways").GetComponent<Transform>();
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        for (int x = 0; x < patrolsPoints.Length; x++)
        {
            patrolsPoints[x].SetParent(wayParent);
        }

        randomNum = Random.Range(0, patrolsPoints.Length);

        currentHealth = enemy.health;
        damage = enemy.damage;
        manaReward = enemy.mana;
    }
    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolsPoints[randomNum].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolsPoints[randomNum].position) < 0.1f)
        {
            randomNum = Random.Range(0, patrolsPoints.Length);
        }
    }

    public override void Attack()
    {
        attack.RotateToPlayer(damage);
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

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            isDamageable.TakeDamage(damage);
        }
    }
}
