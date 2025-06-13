using UnityEngine;

public class BossThird : BossController
{
    private GameObject player;

    private void Awake()
    {
        health = boss.health;

        damage = boss.damage;

        speed = boss.speed;

        manaReward = boss.manaReward;

        spawnRate = boss.spawnRate;

        fireRate = boss.fireRate;

        timer = 0;

        timerSpawn = 0;

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController");

        player = GameObject.FindGameObjectWithTag("Player");

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

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        Attack();
    }

    public override void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public override void Attack()
    {
        if (timer <= 0)
        {

            timer = fireRate;
        }


        if (timerSpawn <= 0)
        {
            boss.SpawnEnemies(childSpawns, gameManagerMazeRunner);

            timerSpawn = spawnRate;
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

            gameManagerMazeRunner.GetComponent<PoolManager>().Return(gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().currentBoss, gameObject);
        }
        else if (health == boss.health / 2)
        {
            damage *= 2;

            speed *= 2;
        }
    }
}
