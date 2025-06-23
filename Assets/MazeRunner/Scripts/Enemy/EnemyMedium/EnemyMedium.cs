using UnityEngine;

public class EnemyMedium : EnemyController
{
    private GameObject player;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        currentHealth = enemy.health;

        damage = enemy.damage;

        manaReward = enemy.mana;

        speed = enemy.speed;

        player = GameObject.FindGameObjectWithTag("Player");

        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";
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

            enemy.PushItems(dropItem.GetComponent<Rigidbody2D>(), Vector2.down, 0.5f);

            enemy.PushItems(dropAmmo.GetComponent<Rigidbody2D>(), Vector2.up, 0.5f);

            player.GetComponent<PlayerController>().killsInRow++;

            if (player.GetComponent<PlayerController>().killsInRow >= 5)
            {
                currentElemental = Instantiate(pickElementalWeapon.SelectRandomObject(), spawnItem.position, Quaternion.identity);

                player.GetComponent<PlayerController>().killsInRow = 0;
            }

            gameObject.SetActive(false);
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
