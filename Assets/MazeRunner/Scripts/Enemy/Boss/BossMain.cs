using System.Collections;
using UnityEngine;

public class BossMain : BossController
{
    [SerializeField] private Transform[] ways;
    [SerializeField] private GameObject enemyBullet;

    private GameObject player;
    private GameObject bullet;
    private Vector2 direction;
    private float angle;
    private int pickRandomWay;
    private float initalAngle;

    private void Awake()
    {
        health = boss.health;

        damage = boss.damage;

        speed = boss.speed;

        manaReward = boss.manaReward;

        spawnRate = boss.spawnRate;

        fireRate = boss.fireRate;

        timerAttack = 0f;

        timer = 0;

        timerSpawn = 0;

        initalAngle = 90f;

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController");

        transform.position = gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().bossSpawn.position;

        for (int x = 0; x < ways.Length; x++)
        {
            ways[x].transform.SetParent(null);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        pickRandomWay = Random.Range(0, ways.Length);

        healthText.text = $"{health} / {boss.health}";

        healthBar.value = health / boss.health;

    }

    private void OnEnable()
    {
        health = boss.health;

        damage = boss.damage;

        speed = boss.speed;

        manaReward = boss.manaReward;

        spawnRate = boss.spawnRate;

        fireRate = boss.fireRate;

        timer = 0;

        timerSpawn = 0;

        transform.position = gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().bossSpawn.position;

        pickRandomWay = Random.Range(0, ways.Length);

        healthText.text = $"{health} / {boss.health}";

        healthBar.value = health / boss.health;

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if (timerSpawn > 0)
        {
            timerSpawn -= Time.deltaTime;
        }

        if (timerAttack > 0)
        {
            timer -= Time.deltaTime;
            timerAttack -= Time.deltaTime;
        }

        Attack();
    }

    public override void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, ways[pickRandomWay].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, ways[pickRandomWay].position) < 0.2f)
        {
            pickRandomWay = Random.Range(0, ways.Length);
        }
    }

    public override void Attack()
    {
        direction = player.transform.position - transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - initalAngle));

        Shoot();

        if (timerSpawn <= 0)
        {
            boss.SpawnEnemies(childSpawns, gameManagerMazeRunner);

            timerSpawn = spawnRate;
        }
    }

    private void Shoot()
    {
        if (timerAttack <= 0)
        {
            attackUI.SetActive(true);
            StartCoroutine(ShootBullet());
        }
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        animationController.SetBool("isHit", true);

        CheckHealth();
    }

    public override void CheckHealth()
    {
        healthBar.gameObject.SetActive(true);

        healthBar.value = health / boss.health;

        healthText.text = $"{health} / {boss.health}";

        animationController.SetBool("isHit", false);

        if (health <= 0)
        {
            gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().deathBoss++;

            boss.SpawnItem(spawnPoint);

            gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().Win();

            gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().SpawnBoss();

            gameManagerMazeRunner.GetComponent<PoolManager>().Return(gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().currentBoss, gameObject);

        }
        else if (health == boss.health / 2)
        {
            damage *= 2;

            speed *= 2;
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

    IEnumerator ShootBullet()
    {
        yield return new WaitForSeconds(1f);

        attackUI.SetActive(false);

        if (timer <= 0)
        {
            bullet = gameManagerMazeRunner.GetComponent<PoolManager>().PoolInstance(enemyBullet);

            bullet.GetComponent<EnemyBullet>().speed = 5f;

            bullet.GetComponent<EnemyBullet>().prefab = enemyBullet;

            bullet.transform.position = shootPoint.position;

            bullet.transform.rotation = gameObject.transform.rotation;

        }
       
        timer = fireRate;
        timerAttack = 3f;
    }
}
