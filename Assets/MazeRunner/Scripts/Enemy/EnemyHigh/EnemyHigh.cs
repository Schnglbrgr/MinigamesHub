using UnityEngine;

public class EnemyHigh : EnemyController
{
    [SerializeField] private Transform[] patrolsPoints;
    [SerializeField] private AttackEnemyHigh attack;

    private GameObject player;
    private Transform wayParent;
    private int randomNum;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        wayParent = GameObject.FindGameObjectWithTag("Ways").GetComponent<Transform>();

        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        player = GameObject.FindGameObjectWithTag("Player");

        for (int x = 0; x < patrolsPoints.Length; x++)
        {
            patrolsPoints[x].SetParent(wayParent);
        }

        randomNum = Random.Range(0, patrolsPoints.Length);

        currentHealth = enemy.health;

        damage = enemy.damage;

        speed = enemy.speed;

        manaReward = enemy.mana;

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        Attack();
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
        attack.AttackPlayer(damage);
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";

        CheckHealth();

        animationController.SetBool("isHit", true);

        audioController.MakeSound(audioController.getHit);
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

            player.GetComponent<PlayerController>().killsInRow++;

            if (player.GetComponent<PlayerController>().killsInRow >= 5)
            {
                currentElemental = Instantiate(pickElementalWeapon.SelectRandomObject(), spawnItem.position, Quaternion.identity);

                player.GetComponent<PlayerController>().killsInRow = 0;
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
