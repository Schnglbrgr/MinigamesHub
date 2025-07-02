using System.Collections;
using UnityEngine;

public class BossSecond : BossController
{
    [SerializeField] private Transform[] ways;

    public bool insideRange;
    private int randomWay;

    private void Awake()
    {
        health = boss.health;

        damage = boss.damage;

        speed = boss.speed;

        manaReward = boss.manaReward;

        fireRate = boss.fireRate;

        healthBar.value = health / boss.health;

        healthText.text = $"{health} / {boss.health}";

        timer = 0;

        timerSpawn = 0;

        spawnRate = boss.spawnRate;

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController");

        randomWay = Random.Range(0, ways.Length);

        for (int x = 0; x < ways.Length; x++)
        {
            ways[x].SetParent(null);
        }

    }
    private void OnEnable()
    {
        health = boss.health;

        damage = boss.damage;

        speed = boss.speed;

        manaReward = boss.manaReward;

        spawnRate = boss.spawnRate;

        fireRate = boss.fireRate;

        healthBar.value = health / boss.health;

        healthText.text = $"{health} / {boss.health}";

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
        transform.position = Vector3.MoveTowards(transform.position, ways[randomWay].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, ways[randomWay].position) < 0.2f)
        {
            randomWay = Random.Range(0, ways.Length);
        }
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            attackUI.SetActive(true);

            animationController.SetBool("isReload", true);

            animationController.SetBool("isAttacking", true);

            StartCoroutine(MakeAttack());
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

            gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().SpawnBoss();

            gameManagerMazeRunner.GetComponent<PoolManager>().Return(gameManagerMazeRunner.GetComponent<GameManagerMazeRunner>().currentBoss, gameObject);

        }
        else if (health == boss.health / 2)
        {
            damage *= 2;

            speed *= 2;
        }
    }

    IEnumerator MakeAttack()
    {
        yield return new WaitForSeconds(5f);

        attackUI.SetActive(false);

        animationController.SetBool("isAttacking", false);

        timer = fireRate;
    }

    public IEnumerator MakeDamage(IDamageable isDamageable)
    {
        while (insideRange)
        {
            isDamageable.TakeDamage(damage);
            yield return new WaitForSeconds(2f);
        }
    }

}
