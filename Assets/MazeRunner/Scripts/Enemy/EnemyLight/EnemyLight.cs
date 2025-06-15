using UnityEngine;

public class EnemyLight : EnemyController
{
    [SerializeField] private Transform[] patrolsPoints;
    [SerializeField] private AttackEnemyLight attack;

    private Transform wayParent;
    private int randomNum;

    private void Awake()
    {
        wayParent = GameObject.FindGameObjectWithTag("Ways").GetComponent<Transform>();

        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        for (int x = 0; x < patrolsPoints.Length; x++)
        {
            patrolsPoints[x].SetParent(wayParent);
        }

        randomNum = Random.Range(0, patrolsPoints.Length);

        currentHealth = enemy.health;

        damage = enemy.damage;

        manaReward = enemy.mana;

        speed = enemy.speed;

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";
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

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";

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

            dropItem = Instantiate(dropRandomItem.SelectRandomObject(), spawnItem.position, Quaternion.identity);

            dropAmmo = poolManager.PoolInstance(ammoPrefab);

            dropAmmo.transform.position = spawnItem.position;

            dropAmmo.GetComponent<AmmoPickUp>().ammoReward = enemy.ammoReward;

            enemy.PushItems(dropItem.GetComponent<Rigidbody2D>(), Vector2.down, 0.5f);

            enemy.PushItems(dropAmmo.GetComponent<Rigidbody2D>(), Vector2.up, 0.5f);

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
